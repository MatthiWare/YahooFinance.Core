![.NET Core](https://github.com/MatthiWare/YahooFinance.Core/workflows/.NET%20Core/badge.svg)
[![AppVeyor](https://ci.appveyor.com/api/projects/status/8a3r5c9rxot4ixed?svg=true)](https://ci.appveyor.com/project/Matthiee/yahoofinance-core)
[![Issues](https://img.shields.io/github/issues/MatthiWare/YahooFinance.Core.svg)](https://github.com/MatthiWare/YahooFinance.Core/issues)
[![License](https://img.shields.io/badge/License-AGPL%20v3-blue.svg)](https://tldrlegal.com/license/apache-license-2.0-(apache-2.0))
[![Nuget](https://buildstats.info/nuget/MatthiWare.YahooFinance)](https://www.nuget.org/packages/MatthiWare.YahooFinance)


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

##### Get Quote

```csharp
var client = new YahooFinanceClient();

// Returns a list of quote results
var result = await client.Quote.LookupAsync("Symbol or list of symbols");
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
