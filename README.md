# YahooFinance.Core
A simple Yahoo Finance API made in .NET Standard

## Usage

##### API

All API calls return an ```IApiResponse<TData>``` object.

##### Using DI

```csharp
using MatthiWare.YahooFinance.Core.Extensions;

services.AddYahooFinance();
```

##### Using constructor

```csharp
using MatthiWare.YahooFinance;

var client = new YahooFinanceClient();
```

##### Searching for Symbol or ISIN

```csharp
var client = new YahooFinanceClient();

// returns a list of search results, first item being the best matching.
var result = await client.Search.SearchAsync("Symbol or ISIN");
```

##### Get Dividend History

```csharp
using NodaTime;

var client = new YahooFinanceClient();

// returns a list of all dividends in a given period.
var result = await client.History.GetDividendsAsync("Symbol", clock.GetCurrentInstant().Minus(Duration.FromDays(365)), clock.GetCurrentInstant());
```

##### Get Stock Splits

```csharp
using NodaTime;

var client = new YahooFinanceClient();

// returns a list of all stock splits in a given period.
var result = await client.History.GetSplitsAsync("Symbol", clock.GetCurrentInstant().Minus(Duration.FromDays(365)), clock.GetCurrentInstant());
```

##### Get Prices History

```csharp
using NodaTime;

var client = new YahooFinanceClient();

// returns a list of all prices in a given period.
var result = await client.History.GetPricesAsync("Symbol", clock.GetCurrentInstant().Minus(Duration.FromDays(365)), clock.GetCurrentInstant());
```
