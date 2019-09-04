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

### But really it's a dumb analogy
[multi-too picture]
[tool box picture]
* Programming languages are not 'a' tool. They **are** a tool box all by them selves. 
* A good language is a well stocked tool box with quality well selected tools.
* You don't need to have 10 other tool boxes, some impoverished, some cluttered etc.

### Spent last 30 years using Smalltalk
[Great tool box!]
* Can learn to really use the tools in there
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
[Last two projects seemed to be 25% of effort]
[Lots of tooling, almost as (more?)  complex than the application]
[On the critical path]
* Docker, Kubernetis
* Arm templates, Teraform, 

### Cloudflare workers envoke a simpler time
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

### Azure

