﻿namespace Raven.Documentation.Parser
{
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.IO;
	using System.Linq;

	using Raven.Documentation.Parser.Data;
	using Raven.Documentation.Parser.Helpers;

	internal class DirectoryCompiler
	{
		private readonly DocumentCompiler _documentCompiler;

		private readonly ParserOptions _options;

		
		public DirectoryCompiler(DocumentCompiler documentCompiler, ParserOptions options)
		{
			_documentCompiler = documentCompiler;
			_options = options;
		}

		public IEnumerable<DocumentationPage> Compile(DirectoryInfo directoryInfo)
		{
			var directoryName = directoryInfo.Name;
			var documentationVersion = directoryName;

			Debug.Assert(Directory.Exists(_options.GetPathToDocumentationPagesDirectory(documentationVersion)));
			Debug.Assert(Directory.Exists(_options.GetPathToDocumentationSamplesDirectory(Language.Csharp, documentationVersion)));
			Debug.Assert(Directory.Exists(_options.GetPathToDocumentationSamplesDirectory(Language.Java, documentationVersion)));

			return CompileDocumentationDirectory(_options.GetPathToDocumentationPagesDirectory(documentationVersion), documentationVersion);
		}

		public IEnumerable<TableOfContents> GenerateTableOfContents(DirectoryInfo directoryInfo)
		{
			var directoryName = directoryInfo.Name;
			var documentationVersion = directoryName;
			var directory = _options.GetPathToDocumentationPagesDirectory(documentationVersion);

			Debug.Assert(Directory.Exists(_options.GetPathToDocumentationPagesDirectory(documentationVersion)));

			var docListFilePath = Path.Combine(directory, Constants.DocListFileName);
			if (File.Exists(docListFilePath) == false)
				yield break;

			foreach (var item in DocListFileHelper.ParseDocListFile(docListFilePath))
			{
				if (!item.IsFolder)
					continue;

				var tableOfContents = new TableOfContents
					                      {
						                      Version = documentationVersion,
						                      Category = CategoryHelper.ExtractCategoryFromPath(item.Name),
						                      Items = GenerateTableOfContentItems(Path.Combine(directory, item.Name), item.Name).ToList()
					                      };

				yield return tableOfContents;
			}
		}

		private static IEnumerable<TableOfContents.TableOfContentsItem> GenerateTableOfContentItems(string directory, string keyPrefix)
		{
			var docListFilePath = Path.Combine(directory, Constants.DocListFileName);
			if (File.Exists(docListFilePath) == false)
				yield break;

			foreach (var item in DocListFileHelper.ParseDocListFile(docListFilePath))
			{
				var tableOfContentsItem = new TableOfContents.TableOfContentsItem
					                          {
						                          Key = keyPrefix + "/" + item.Name, 
												  Title = item.Description,
												  IsFolder = item.IsFolder
					                          };

				if (tableOfContentsItem.IsFolder)
					tableOfContentsItem.Items = GenerateTableOfContentItems(Path.Combine(directory, item.Name), tableOfContentsItem.Key).ToList();

				yield return tableOfContentsItem;
			}
		}

		private DocumentationPage CompileDocumentationPage(FolderItem page, string directory, string documentationVersion)
		{
			var path = Path.Combine(directory, page.Name + FileExtensionHelper.GetLanguageFileExtension(page.Language) + Constants.MarkdownFileExtension);
			var fileInfo = new FileInfo(path);

			if (fileInfo.Exists == false)
				throw new FileNotFoundException(string.Format("Documentaiton file '{0}' not found.", path));

			return _documentCompiler.Compile(fileInfo, page, documentationVersion);
		}

		private IEnumerable<DocumentationPage> CompileDocumentationDirectory(string directory, string documentationVersion)
		{
			var docListFilePath = Path.Combine(directory, Constants.DocListFileName);
			if (File.Exists(docListFilePath) == false)
				yield break;

			foreach (var item in DocListFileHelper.ParseDocListFile(docListFilePath))
			{
				if (item.IsFolder)
				{
					foreach (var page in CompileDocumentationDirectory(Path.Combine(directory, item.Name), documentationVersion))
					{
						yield return page;
					}

					continue;
				}

				foreach (var pageToCompile in GetPages(directory, item))
				{
					yield return CompileDocumentationPage(pageToCompile, directory, documentationVersion);
				}
			}

			var indexFilePath = Path.Combine(directory, "index" + Constants.MarkdownFileExtension);
			if (File.Exists(indexFilePath) == false)
				yield break;

			var indexItem = new FolderItem(isFolder: false)
				                {
					                Description = string.Empty,
					                Language = Language.All,
					                Name = "index"
				                };

			yield return CompileDocumentationPage(indexItem, directory, documentationVersion);
		}

		private static IEnumerable<FolderItem> GetPages(string directory, FolderItem item)
		{
			var path = Path.Combine(directory, item.Name + Constants.MarkdownFileExtension);
			if (File.Exists(path))
			{
				yield return item;
				yield break;
			}

			var languageFileExtensions = FileExtensionHelper.GetLanguageFileExtensions();

			foreach (var language in languageFileExtensions.Keys)
			{
				var extension = languageFileExtensions[language];
				var name = item.Name + extension;
				path = Path.Combine(directory, name + Constants.MarkdownFileExtension);
				if (File.Exists(path))
				{
					yield return new FolderItem(item)
									 {
										 Language = language
									 };
				}
			}
		}
	}
}