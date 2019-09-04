## Intro Slide
[ me, title, tag line]
* First talk so did some research
* Three ways to start a talk
  * Repeat introduction - hi I'm Joerg .... But you just heard that so you'll go back to Twitter
  * May a joke .... sounds good in theory but that is it's own profession
  * Tell a story .... So that's what I decided to do

* That was the story...and the joke?

## Motivation
[This talk is about hosting Fable on other run times]
[In particular why Cloudflare workers (and if I ever get to it CosmosDB)]
[ So what is the story, or better yet the motivation]

* So seriously, what got me up here?
* Two things

### I just like to use a hammer
[hammer nail picture]
* Seriously I don't believe in using the best tool for the job
* Spend the 

### But really it's a misleading analogy
[multi-tool picture]
[tool box picture]
* Programming languages are not 'a' tool. They **are** a tool box all by them selves. 
* A good language is a well stocked tool box with quality well selected tools.
* You don't need to have 10 other tool boxes, some impoverished, some cluttered etc.

### Focus on the wrong thing
[To much on benefits of just the right language]
[Not enought on value of true mastery of ones toolbox]
* I can order breakfast in 15 languages
[Also not enough on benefits of common skills in a group]
* Spent last 30 years using Smalltalk. Great tool box!
* Over time team can learn to really use the tools in there
* Can use those tools for pretty much everything
    * Modeling
    * Application development
    * DevOps
    * CI and Source control
    * Development of the language itself including a high performance JIT

### Which brings me to Fable
* We should select a language with a great set of tools and then invest in developing expertise
* Love it to death but at some point wanted to move on and felt there was merit in a typed functional approach. 
    * Feel it can do for state what GC did for memory managment, lift the burden on the developer. 
    * Elmish architecture is a key data point here
* Picked F# as alLanguage with great tool kit, powerful concepts pleasant to use
* But of course I want to apply it everywhere, hence Fable.

### Our **DevOps** is out of control
[Reason two]
[Last two projects seemed to be 25% of effort]
[Lots of tooling, almost as (more?)  complex than the application]
[On the critical path]
[Lots of mission critical knowledge]
* Docker, Kubernetis
* Arm templates, Teraform, 

### Cloudflare workers envoke a simpler time
[Sometimes you need all the DevOps complexity] 
* perhaps less often that we think
[Service worker API in the cloud]
* simple api 
[Javascript (+WASM)]
[Always on, globally deployed by default]
* they have CDN with local POP in 193 countries. Deployng is just copying that text file arround.

### Demo of a worker in JS
[cut to cloudflare pannel]

## So what are they Architectully?
[diagram of them vs heaveri weith runtimes]
[limitations]

## What does it take to get Fable running there
[Not much]
[just need to model the request and response]
[then return a promise (aka js async)]

### Demo of hello world 
[Code up in Fable]
[Run compiler]
[Paste into Worker pannel]
* it works!
* appache perf

## That's it really
[hard work has been done by the community]
[Bit of refacory]
[Need a bit of tooling]
[running locally]
[cli deployment]

### Demo of API

### KV Store
* Key Value store
* Global
* Evenually consistent
* High read performance, meh write
* Expiring keys
* Prefix search (model file herarchy?)
* Great for secrets (environment vars for api)
* Even for data (100 meg value size, billions of keys)

## What is it good for?
* Expiring keys perfect for todo list (at least mine)
* Simple services
* Idea is make a call rather than load a library and data (net work trade off)
  * validation services
  * character normalization
  * Lists that need updateing

* Simple IDE based on javascript version of Fable
* Port a "static" site generator
* Compiler service?

## Cosmos DB
[Most obvious target]
[Program Functions, Stored Procedures and Triggers]
* Said I would get back to that
[Original motivation was moaning about this code]
* really actually quite simple lazy concurrency control
  * given a list of items and their etags when orignally read
  * re-read from DB and ensure etags are unchanged
  * update docs
  * commit


### But...
[Signs the Interop will be harder]
[Bigger API and very old style]
* Babel targets may polyfill may handle?
[MS recently updated external JS programming model]
[waiting for the penny to drop on internal JS]