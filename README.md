# Blazor QandA
A sample Q&A application built with Blazor WebAssembly

## Project Overview

* QA.Domain: contains the domain entities and service interfaces
* QA.DemoServices: implementation of the service interfaces using dummy in-memory data. The initial set of questions is generated using Bogus and WaffleEngine. 
* QA.Web.Client: the Blazor client-side application
* QA.Web.Service: the WebAPI project that also delivers the webassembly client