# YahooFinance.Core
A simple Yahoo Finance API made in .NET Standard

## Usage

##### Using DI

```csharp
using MatthiWare.YahooFinance.Core.Extensions;

services.AddYahooFinance();
```

##### Using constructor

```csharp
using MatthiWare.YahooFinance.Core;

var client = new YahooFinanceClient();
```

##### Searching for Symbol or ISIN

```csharp
var client = new YahooFinanceClient();

var result = await client.Search.SearchAsync("Symbol or ISIN");

// returns a list of search results, first item being the best matching.
```
