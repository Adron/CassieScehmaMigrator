# Cassie Schema Migrator

[![Build Status](https://dev.azure.com/adronhall-datastax/cassie-schema-migrator/_apis/build/status/Adron.CassieSchemaMigrator?branchName=master)](https://dev.azure.com/adronhall-datastax/cassie-schema-migrator/_build/latest?definitionId=4&branchName=master) [![GitHub issues](https://img.shields.io/github/issues/Adron/CassieSchemaMigrator.svg)](https://github.com/Adron/CassieSchemaMigrator/issues) [![GitHub issues by-label](https://img.shields.io/github/issues-raw/Adron/CassieSchemaMigrator/good%20first%20issue.svg)](https://github.com/Adron/CassieSchemaMigrator/issues?q=is%3Aissue+is%3Aopen+label%3A%22good+first+issue%22) [![contributions welcome](https://img.shields.io/badge/contributions-welcome-brightgreen.svg?style=flat)](https://github.com/Adron/CassieSchemaMigrator/issues)

[![Twitter Follow](https://img.shields.io/twitter/follow/Adron.svg?style=social)](https://twitter.com/Adron)

Cassie Schema Migrator (CSM) tool for running Apache Cassandra and DataStax Enterprise (DSE) schema migrations.

## CSM = Cassie Schema Migrator

Mission: Building an application using C# to do Apache Cassandra / DSE Data Schema Migrations.

## Prerequisites

I've made the assumption, for the time being, that Apache Cassandra is installed and available - single node or a full cluster - at 127.0.0.1. With that installed all tests should pass for development purposes. However it would be good to change this so the value is derived from a configuration setting so the dev database can be setup anywhere.

