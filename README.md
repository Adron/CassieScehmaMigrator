# Cassie Scehma Migrator

Cassie Schema Migrator (CSM) tool for running Apache Cassandra and DataStax Enterprise (DSE) schema migrations.

## CSM = Cassie Schema Migrator

Mission: Building an application using C# to do Apache Cassandra / DSE Data Schema Migrations.

#### Core Functionality

* App will take cql files based on date, time, and *up* or *down* keyword for migrations up of data schema.
* App will execute the cql files in order dated by date time.
* App will maintain a table of up or down state of the database keyspace.
* App will provide a way to create the initial keyspace based on various requirements on configuration such as strategy, replication factor, etc.
* App will store connection information pertinent to the database that it is or needs to be working against.
* App will have the ability to have the connection information passed in if this works more effectively for automation.
* App will initially provide a console based CLI style interaction model.
* App will not require any Apache Cassandra related CLI tools, drivers, or other elements on the machine that it executes from.
* We will intend to build out some multi-interface options for this application once the CLI is built out and operative.

#### Environmental Tasks

* Setup a build for our application.
* Determine outputs for Mac OS, Linux, and Windows.
* Setup documentation for our application.
* Local Docker container for Apache Cassandra to work from.