<p align="center">
    <a href="https://ci.appveyor.com/project/Matthiee/yahoofinance-core"><img src="https://ci.appveyor.com/api/projects/status/8a3r5c9rxot4ixed?svg=true" alt="Build Status (AppVeyor)"></a>
    <a href="https://github.com/MatthiWare/YahooFinance.Core/issues"><img src="https://img.shields.io/github/issues/MatthiWare/YahooFinance.Core.svg" alt="Open Issues"></a>
    <a href="https://tldrlegal.com/license/apache-license-2.0-(apache-2.0)"><img src="https://img.shields.io/badge/License-AGPL%20v3-blue.svg" alt="AGPL v3"></a>
    <a href="https://www.nuget.org/packages/MatthiWare.YahooFinance"><img src="https://buildstats.info/nuget/MatthiWare.YahooFinance" alt="NuGet badge"></a>
</p>

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
