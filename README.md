[![Travis](https://img.shields.io/travis/lorenzlars/poco-query.svg)](https://travis-ci.org/lorenzlars/poco-query)
[![Github Issue](https://img.shields.io/github/issues/lorenzlars/poco-query.svg?maxAge=3600)](https://github.com/lorenzlars/poco-query/issues) 
[![GitHub Fork](https://img.shields.io/github/forks/lorenzlars/poco-query.svg?maxAge=3600)](https://github.com/lorenzlars/poco-query/network) 
[![GitHub Star](https://img.shields.io/github/stars/lorenzlars/poco-query.svg?maxAge=3600)](https://github.com/lorenzlars/poco-query/stargazers)

![](https://img.shields.io/static/v1?label=.NET+Framework&message=4.5.2&color=informational)

# POCOQuery

**POCOQuery is a extention library to the `System.Linq.IQueryable` interface.**

The extension is for query with a POCO against a IQueriable source to obtain a separation between source and destination.
As an example with the Entity Framework, you can hide joins against the consuming client.

### Create a POCO object

```csharp
using POCOQuery.Attributes;

[Entity(Type = typeof(DatabaseContext.User)]
public class User
{
    [EntityProperty(Binding ="x.id")]
    public long Id { get; set; }
    [EntityProperty(Binding = "x.name")]
    public string Name { get; set; }
    [EntityProperty(Binding = "x.address.street")]
    public string Street { get; set; }
}
```

### Request a POCO with Entity Framework

``` csharp
using POCOQuery.Filter;
using POCOQuery.Order;
using POCOQuery.Select;

using (var context = new UserEntities())
{
    FilterDefinition filterDefinition = new FilterDefinition { 
        Operator = Operator.Contains,
        Property = "Id",
        Value = 1
    };

    OrderDefinition orderDefinition = new OrderDefinition {
        Property = "Id",
        Direction = ListSortDirection.Ascending
    };

    IList<User> users = context.User
        .FilterBy<DatabaseContext.User, User>(filterDefinition)
        .OrderBy<DatabaseContext.User, User>(orderDefinition)
        .Select<DatabaseContext.User, User>();
}
```
