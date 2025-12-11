# TriviaApp
      
A trivia application based on the Open Trivia Database API built with Angular and .NET 9, serving as both a functional project and a personal reference for common patterns and practices.

(Add screenshot of final product here)

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

## Future Enhancements
- JWT-based authentication, with `Microsoft.AspNetCore.Identity` as endpoint protection on the backend, and AuthGuards / HttpInterceptors on the front-end.
- Postgres Database with `EntityFramework` as ORM.
- CI/CD pipeline (GitHub Action) for automatic auditting, testing, building & deploying.