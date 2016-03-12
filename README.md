# MR.AspNet.Identity.EntityFramework6

[![Build status](https://img.shields.io/appveyor/ci/mrahhal/mr-aspnet-identity-entityframework6/master.svg)](https://ci.appveyor.com/project/mrahhal/mr-aspnet-identity-entityframework6)
[![NuGet version](https://badge.fury.io/nu/MR.AspNet.Identity.EntityFramework6.svg)](https://www.nuget.org/packages/MR.AspNet.Identity.EntityFramework6)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](https://opensource.org/licenses/MIT)

EntityFramework 6 provider for Identity 3.0 RC1.

## What is this?

This is basically a copy-paste from the EF7 Identity 3.0 RC1 provider but edited to work with EF6. Furthermore it contains in memory stores to be used in unit tests. **However**, this is not a 1 to 1 port as some generic structures have been tweaked. All you have to do in your app:

- Remove everything EF7 related, this means: `EntityFramework.Commands`, `EntityFramework.MicrosoftSqlServer`, `EntityFramework.InMemory` and `Microsoft.AspNet.Identity.EntityFramework`. And instead add the following: `EntityFramework` and `MR.AspNet.Identity.EntityFramework6`.
- Replace all EF7 namespaces with their EF6 counterparts.


## `MR.AspNet.Identity.EntityFramework6` [![NuGet version](https://badge.fury.io/nu/MR.AspNet.Identity.EntityFramework6.svg)](https://www.nuget.org/packages/MR.AspNet.Identity.EntityFramework6)

The port of `Microsoft.AspNet.Identity.EntityFramework` to work under EF6.

## `MR.AspNet.Identity.EntityFramework6.InMemory` [![NuGet version](https://badge.fury.io/nu/MR.AspNet.Identity.EntityFramework6.InMemory.svg)](https://www.nuget.org/packages/MR.AspNet.Identity.EntityFramework6.InMemory)

Contains in memory implementations of `IUserStore` and `IRoleStore` to be used in unit tests.

**Note:** This has a dependency on [`MR.Patterns.Repository`](https://github.com/mrahhal/https://github.com/mrahhal/MR.Patterns.Repository) for the in memory repository implementation.

## Do you need ef6 migrations enabled for your Asp.Net Core app?

Check out [Migrator.EF6](https://github.com/mrahhal/Migrator.EF6).
