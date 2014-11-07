# How the indexes work

In order to allow fast queries over your indexes, RavenDB processes them in the background, executing the queries against the stored documents and persisting the results to a Lucene index. [Lucene](http://lucene.apache.org/core/index.html) is a full text search engine library (Raven uses the .NET version) which allows us to perform lightning fast full text searches.

The best way of thinking about RavenDB's indexes is to imagine them as a database's materialized views. RavenDB executes the indexing processes in the background, and the results are written to disk. That means that when we are querying, we have to do very little work. This is how RavenDB manages to achieve its near instantaneous replies for your queries, it doesn't have to think, all the processing has already been done.

By using Lucene as the indexing format, we can support some really fancy querying types: Range based, partial string matching, full text searches, etc. You can read more about queries supported by Lucene here.

If you are using the Raven Client API, you can simply use the Linq provider that comes with it and it will deal with most of those issues for you. If you want to query RavenDB externally, or if you want to understand how Raven manages those indexes and take advantage of advanced Lucene features, please read on.

When RavenDB needs to store the results of your queries in the Lucene index, it analyzes each value, and produce the following results:

* If the value is null - create a single field with the unanalyzed value 'NULL_VALUE'.
* If the value is string - create a single field with the supplied name and the value.
* If the value is set unanalyzed - create a single field with the value set to not analyzed.
* If the value is date, create a single field with millisecond precision with the supplied name.
* If the value is numeric (int, long, double, decimal, or float) will create two fields:
* * The first will be created with the supplied name, containing the numeric value as an unanalyzed string. This is useful if you want to query the by the exact value.
* * The second will be create with the name: '[name]_Range', containing the numeric value in a form that allows range queries.
* * Sample, if we try to index 'Age', 18, we will have the following fields:
* * * Age:18
* * * Age_Range: [18 in a binary format that is applicable for range searching]

Using this format, it is pretty easy to perform both exact queries and range queries, including when you need to detect nulls.

##Using custom analyzers

Lucene uses Analyzers to split up text into the tokens that are then stored in the index. Normally the default analyzers is okay, but Raven lets you specify which built-in Lucene analyzer to use, in the case when the default analyzer isn't suitable. You can control the analyzer per-field like so:

    store.DatabaseCommands.PutIndex("Movies",
            new IndexDefinition
            {
                Map = "from movie in docs.Movies select new { movie.Name, movie.Tagline }",
                Analyzers =
                    {
                        {"Name", typeof(SimpleAnalyzer).FullName},
                        {"Tagline", typeof(StopAnalyzer).FullName},                                    
                    }
            });

The output of the built-in analyzers are shown below, they are all tokenizing the following text:

    The quick brown fox jumped over the lazy dog, bob@hotmail.com 123432.

* **Keyword Analyzer** - tokenizes the entire stream as a single token.
    
 [The quick brown fox jumped over the lazy dog, bob@hotmail.com 123432.]

* **Whitespace Analyzer** - tokenizes on white space only (note the punctuation at the end of "dog")

    [The]   [quick]   [brown]   [fox]   [jumped]   [over]   [the]   [lazy]   [dog,]   [bob@hotmail.com]   [123432.]

* **Stop Analyzer** - strips out common English words (such as "and", "at" etc), tokenizes letters only and converts everything to lower case

    [quick]   [brown]   [fox]   [jumped]   [over]   [lazy]   [dog]   [bob]   [hotmail]   [com]

* **Simple Analyzer** - only tokenizes letters and makes all tokens lower case

    [the]   [quick]   [brown]   [fox]   [jumped]   [over]   [the]   [lazy]   [dog]   [bob]   [hotmail]   [com]

* **Standard Analyzer** - simple tokenizer that uses a stop list of common English works, also handles numbers and emails addresses correctly

    [quick]   [brown]   [fox]   [jumped]   [over]   [lazy]   [dog]   [bob@hotmail.com]   [123432]

A simple way to see how text is tokenized by the Lucene analyzers is to use the tool available [here](http://www.codeproject.com/Articles/32175/Lucene-Net-Text-Analysis).

You can also create your own custom analyzer, compile it to a dll and drop it in in directory called "Analyzers" under the RavenDB base directory. Afterward, you can then use the fully qualified type name of your custom analyzer as the analyzer for a particular field.