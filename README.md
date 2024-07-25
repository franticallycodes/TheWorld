# The World
An API that integrates a small subset of the REST Countries API

It is currently deployed on Azure [here](https://thisdotmartintest.azurewebsites.net/swagger).

#### Description of your approach and implementation.
Since my experience is with .NET that's what I used to implement this API. The architecture remained small due to the scope of the application, so instead of artificially inflating it, I decided to spend my time showcasing more of the things that I'm capable of.
#### Highlight something you found interesting or significant.
The data itself was pretty interesting to browse, for instance I didn't know that Hong Kong is a separate country, but from a technical standpoint there were some notable differences in the API responses. One example is that accessing `/lang/{code}` with an invalid value would return a 404, but accessing `/alpha/{code}` with an invalid value would return a 400.
#### What you are most pleased or proud of.
I guess I'm most pleased with the use of extension methods. I originally went with a service to hold the query logic, like I might in a real world application, but I think it looks much nicer as chained methods.
#### What feature or improvement you would add given more time
I ended up ditching the Dockerfile at the end because I ran into an issue getting it to work with the .NET minimal hosting. I've used Docker before for established projects and images, but I don't have nearly as much experience troubleshooting containerization as I'd like. That's something I'll continue to experiment with.

## Running Locally
You'll need to add a new value to your appsettings file with the name of `ApiBaseUrl` and the value of `https://restcountries.com/v3.1/`. After that a simple `dotnet run` from the project folder should get you going. Once the project is running you should be able to access the Swagger interface at [https://localhost:7165/swagger]().