# Sreeni.OData.Transpiler.Core

Sreeni.OData.Transpiler.Core is a .Net based OData transpiler that translates OData queries to downstream data source queries like MySQL, Postgres, MS SQL, Azure Cosmos, etc.

The objective is to enable seamless integration and query translation across different database platforms.  

This package has core contracts and infrastructure designed to translate OData queries into database-specific queries for multiple database systems. 

## Features

- Parse OData queries
- Convert OData queries to SQL-like queries
- Supports multiple database systems:  
  - [Azure Cosmos DB - Completed](https://www.nuget.org/packages/Sreeni.OData.Transpiler.Azure.Cosmos)
  - MS SQL(Open)
  - MySQL(Open)
  - PostgreSQL(Open)
  - MongoDB(Open)
  - And more
- Support for common OData query options: `$select`, `$filter`, `$orderby`, `$top`, `$skip`, and `$expand`
- Nested fields in the select clause, filter expressions, and orderby expressions

## Getting Started
### Installation

To install the package, add the following dependency to your project:
```bash
dotnet add package Sreeni.OData.Transpiler.Core
```

### Usage

This package primarily required to build your own OData transpiler for a specific database system. The following steps are required to build a transpiler for a specific database system.

1. Create a class that implements the `IODataQueryTranslator` interface.
2. Implement the `Translate` method from `IODataQueryTranslator` interface.
3. Use `ODataQueryParser` to parse the given OData query.
```csharp
   public class AzureCosmosODataQueryTranslator : IODataQueryTranslator
   {
	   public QueryResult Translate(string odataQuery)
	   {
		   // Validate the query
		   ...
		   var parser = new ODataQueryParser();
		   ODataQuery query = parser.Parse(odataQuery);
		   // Translate OData query to Cosmos SQL query
		   ...
		   Create visitor for each OData query option like $select, $filter, $orderby, $top, $skip, and $expand and use them here
		   ...
		   // Return the translated query
		   return queryResult;
	   }
   }
   ```
4. The library uses the visitor pattern to handle different parts of the OData query. The following visitors are required for each database query translator:

- `SelectVisitor`: Requires to handle the `$select` option
- `FilterVisitor`: Requires to handle the `$filter` option
- `OrderByVisitor`: Requires to handle the `$orderby` option
- `TopVisitor`: Requires to handle the `$top` option
- `SkipVisitor`: Requires to handle the `$skip` option
- `ExpandVisitor`: Requires to handle the `$expand` option
5. Implement the visitor classes for each OData query option like `$select`, `$filter`, `$orderby`, `$top`, `$skip`, and `$expand`.
   - Use the visitor classes to translate the OData query to the database-specific query.
6. Create a database specific client to execute the translated query.
	- Implement the `IODataClient` interface. It has `GetItemByQueryAsync` method to return single item, if you want to list of items then you need to implement `IODataListClient` interface.
	```csharp

	public class AzureCosmosODataClient : IODataClient
	{
		public async Task<T> GetItemByQueryAsync<T>(string query)
		{
			// Execute the query and return the result
			...
		}
	}
	//To get list of items
	public class AzureCosmosODataClient : IODataListClient
	{
		public async Task<T> GetItemByQueryAsync<T>(string query)
		{
			// Execute the query and return the result
			...
		}
		public async Task<IEnumerable<T>> GetListByQueryAsync<T>(string query)
		{
			// Execute the query and return the result
			...
		}
	}
	```
## Implemented Translators
- Azure Cosmos DB and find the package from [here](https://www.nuget.org/packages/Sreeni.OData.Transpiler.Azure.Cosmos).

## Contributing

Contributions are welcome! Please open an issue or submit a pull request on GitHub.

To contribute(via Pull Request):  
1. Fork the repository.  
2. Create a new branch with a descriptive name.  
3. Make your changes and commit them with clear and concise messages.  
4. Push your changes to your forked repository.  
5. Submit a pull request.  
  
Please ensure your code follows the project's coding standards and passes all tests.  

## License

This project is licensed under the MIT License.