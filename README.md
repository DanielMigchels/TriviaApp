# TriviaApp
      
A trivia application based on the Open Trivia Database API built with Angular and .NET 9, serving as a personal reference for common patterns and practices.

<img style="width: 600px;" src="TriviaApp.Docs/demo.gif">

## How to Run

Instructions on how to run the application.

### Docker Compose
Pulls image from public container registry, and runs it along with its dependencies on your docker instance.
```bash
docker-compose up
```

Compiles source code, builds docker image, and runs it along with its dependencies on your docker instance.
```bash
docker-compose -f docker-compose.local.yml up
```
App becomes available on port 8080 and should be reachable through HTTP. (http://localhost:8080)

### Helm Chart
Installs the app on your Kubernetes cluster.
```bash
helm install triviaapp .\\TriviaApp.Helm\\ --namespace triviaapp --create-namespace
```
App becomes available on port 32050 and should be reachable through HTTP. (http://localhost:32050)

## Architecture

For the datasource, I've chosen Redis as a distributed cache to store correct answers. This keeps the application horizontally scalable for high traffic while maintaining small storage footprint through automatic cache entry expiration.

## Future Enhancements
- Work around Open Trivia API rate limit to optimize user experience
  - Microservice with different public IPs to fool the Trivia rate limiter :)
  - By caching questions, and using those if the server is unavailable.
  - Different or multiple question providers
- JWT-based authentication, with `Microsoft.AspNetCore.Identity` as endpoint protection on the backend, and AuthGuards / HttpInterceptors on the front-end.
- Postgres Database with `EntityFramework` as ORM.
- Pipeline for automatic auditing, testing, building & deploying.
- Add log-sink to ELK stack
- Background tasks with Hangfire or Quartz
- Message Queueing with RabbitMQ (MassTransit)