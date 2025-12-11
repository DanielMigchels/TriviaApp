# TriviaApp
      
A trivia application based on the Open Trivia Database API built with Angular and .NET 9, serving as both a functional project and a personal reference for common patterns and practices.

<img style="width: 600px;" src="TriviaApp.Docs/demo.gif">

## How to Run

Instructions on how to run the application.

### Docker Compose
Compiles source code, builds docker image, and runs it along with its dependencies on your docker instance.
```bash
docker-compose up
```

### Helm Chart
Installs the app on your Kubernetes cluster.
```bash
helm install triviaapp ./charts/triviaapp
```

## Architecture

For the datasource, I've chosen Redis as a distributed cache to store correct answers. This keeps the application horizontally scalable for high traffic while maintaining small storage footprint through automatic cache entry expiration.

## Future Enhancements
- Work around Open Trivia API rate limit to optimize user experience
  - Microservice with different public IP's to fool the Trivia rate limiter :)
  - By caching questions, and using those if the server is unavailable.
  - Different or multiple question providers
- JWT-based authentication, with `Microsoft.AspNetCore.Identity` as endpoint protection on the backend, and AuthGuards / HttpInterceptors on the front-end.
- Postgres Database with `EntityFramework` as ORM.
- Pipeline for automatic auditting, testing, building & deploying.
- Add log-sink to ELK stack
- Background tasks with Hangfire or Quarts
- Message Queueing with RabbitMQ (MassTransit)