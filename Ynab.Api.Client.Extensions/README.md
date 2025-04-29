# YNAB.API.Client.Extensions

A collection of helpful extension methods for the [YNAB.API.Client](https://github.com/tombly/ynab-api-client). The version number generally matches that of the API client which is based on the [YNAB API version](https://api.ynab.com/#changelog). 

## Usage

Add the nuget to your app:

```shell
dotnet add package Ynab.Api.Client.Extensions
```

Import the namespace and call an extension method.

```csharp
using Ynab.Api.Client.Extensions;

...

var budgetDetail = await _ynabApiClient.GetBudgetDetailAsync();
```

## License

Copyright (c) 2025 Tom Bulatewicz

Licensed under the MIT license