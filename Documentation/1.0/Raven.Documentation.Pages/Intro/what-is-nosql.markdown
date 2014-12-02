﻿
## What is NoSQL?

A NoSQL database offers an alternative to the RDBMS style of data storage (**R**elational **D**ata**b**ase **M**anagement **S**ystem), shedding the rigid, table-based system used in an RDBMS, in place of a different storage model that emphasizes higher scalability and availability over conformant to a relational model.

For a more expanded answer see this from [Wikipedia](http://en.wikipedia.org/wiki/NoSQL):

A NoSQL database provides a simple, lightweight mechanism for storage and retrieval of data that provides higher scalability and availability than traditional relational databases. The NoSQL data stores use looser consistency models to achieve horizontal scaling and higher availability.

NoSQL database systems are often highly optimized for retrieval and appending operations and often offer little functionality beyond record storage. The reduced run-time flexibility compared to full SQL systems is compensated by marked gains in scalability and performance for certain data models.

In short, NoSQL database management systems are useful when working with a huge quantity of data (especially big data) when the data's nature does not require a relational model. The data can be structured, but NoSQL is used when what really matters is the ability to store and retrieve great quantities of data, not the relationships between the elements. Usage examples might be to store millions of key–value pairs in one or a few associative arrays or to store millions of data records. This organization is particularly useful for statistical or real-time analysis of growing lists of elements (such as Twitter posts or the Internet server logs from a large group of users).

Other usages of this technology are related with the flexibility of the data model; a lot of applications might gain from this unstructured data model: tools like CRM, ERP, BPM, etc, could use this flexibility to store their data without performing changes on tables or creating generic columns in a database. These databases are also good to create prototypes or fast applications, because this flexibility provides a tool to develop new features very easily.

### Classes of NoSQL Solutions

Most NoSQL solutions store data using one of the following models:

* Document Database
* Key-Value store
* Graph Database

While in theory, any solution can be used to store any type of data, different data storage scenarios are often better suited to one of the available models.

RavenDB is a powerful and scalable document database.