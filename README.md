# MR.AspNet.Identity.EntityFramework6

[![Build status](https://img.shields.io/appveyor/ci/mrahhal/mr-aspnet-identity-entityframework6/master.svg)](https://ci.appveyor.com/project/mrahhal/mr-aspnet-identity-entityframework6)
[![Nuget version](https://img.shields.io/nuget/v/MR.AspNet.Identity.EntityFramework6.svg)](https://www.nuget.org/packages/MR.AspNet.Identity.EntityFramework6)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](https://opensource.org/licenses/MIT)

EntityFramework 6 provider for Identity 3.0 RC1.

## What is this

This is basically a copy-paste from the EF7 Identity 3.0 RC1 provider but edited to work with EF6. All you have in your app:

- Remove everything EF7 related, this means: `EntityFramework.Commands`, `EntityFramework.MicrosoftSqlServer`, `EntityFramework.InMemory` and `Microsoft.AspNet.Identity.EntityFramework`. And instead add the following: `EntityFramework` and `MR.AspNet.Identity.EntityFramework6`.
- Replace all EF7 namespaces with their EF6 counterparts.

## Do you need ef6 migrations enabled for your Asp.Net Core app?

Check out [Migrator.EF6](https://github.com/mrahhal/Migrator.EF6).
