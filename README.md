# QuantumCartAI

**AI-Powered Microservices E-Commerce Platform**  
Modern, scalable, and AI-enhanced e-commerce backend platform built with .NET 8 microservices architecture. 
The platform follows Clean Architecture, Domain-Driven Design (DDD), and event-driven patterns to ensure maintainability, testability, and independent scaling of services. It is designed for high-traffic retail scenarios while supporting seamless guest (anonymous) and authenticated user flows.

<p align="center">
  <img src="https://img.shields.io/badge/.NET-8.0-blue?style=for-the-badge&logo=.net" alt=".NET 8" />
  <img src="https://img.shields.io/badge/Docker-Containerized-blue?style=for-the-badge&logo=docker" alt="Docker" />
  <img src="https://img.shields.io/badge/Architecture-Microservices-orange?style=for-the-badge" alt="Microservices" />

</p>


## ✨ Key Features 

### Implemented / In Progress

- **Anonymous guest sessions** — persistent cart & behavior tracking without login
- **Seamless guest → registered conversion** (cart merge, viewed items, AI profile)
- **Product catalog** with basic CRUD & search foundation
- **Shopping cart** service with real-time discount application
- **YARP API Gateway** — unified routing, CORS, credential forwarding
- **Secure session middleware** — HttpOnly cookie + Redis identity
- **Cross-service shared libraries** — domain models, application abstractions, infrastructure helpers
- **Docker Compose** local environment (Redis, RabbitMQ, SQL Server)


## 🛠 Technology Stack

| Layer                     | Technology                              | Notes / Version                     |
|---------------------------|-----------------------------------------|-------------------------------------|
| Runtime                   | .NET 8 (LTS)                            | Modern minimal APIs, AOT-ready      |
| Web / API                 | ASP.NET Core 8                          | REST + gRPC                         |
| Gateway                   | YARP                                    | Reverse proxy & routing             |
| Messaging                 | RabbitMQ                                | Event bus / eventual consistency    |
| Cache & Sessions          | Redis                                   | Distributed, TTL, JSON support      |
| ORM                       | Entity Framework Core                   | SQL Server / PostgreSQL             |
| NoSQL / Document          | MongoDB (planned)                       | Flexible catalog attributes         |
| AI / ML foundation        | ML.NET + OpenAI/Azure AI integration    | Recommendations & chat              |
| Containerization          | Docker 24+ · Docker Compose v2          | Multi-container local env           |
| Shared code               | Project references / private NuGet      | Domain · Application · Infrastructure |
| Observability (planned)   | OpenTelemetry · Prometheus · Grafana    | Distributed tracing & metrics       |

## 📂 Project Structure
```text
QuantumCartAI/
├── BuildinBlocks/
│   └── BuildingBlocks/               # CQRS, Global error handling
│   └── BuildingBlocks.Messaging/     # RabbitMQ, MassTransit, Events
├── ApiGateway/
│   └── YarpApiGateway/               # YARP config, routes, transforms
├── Services/
│   ├── CartService/
│   ├── CatalogService/
│   ├── OrderingService/
│   ├── InventoryService/
│   ├── AIRecommendationService/
│   ├── ChatService/
│   ├── UserService/
│   └── ... (Discount, Payment, Notification, Analytics)
├── Shared/
│   ├── Domain/                       # Entities, aggregates, value objects
│   ├── Application/                  # Use cases, commands, queries
│   └── Infrastructure.AspNetCore/    # Middleware, CurrentSession, extensions
├── docker-compose.yml
├── docker-compose.override.yml
├── .gitignore
└── README.md
```

