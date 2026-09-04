# 🛒 Full-Stack E-Commerce DevOps Deployment

![Docker](https://img.shields.io/badge/Docker-Containerized-2496ED?logo=docker&logoColor=white)
![Docker Compose](https://img.shields.io/badge/Docker%20Compose-Orchestrated-2496ED?logo=docker&logoColor=white)
![AWS EC2](https://img.shields.io/badge/AWS-EC2-FF9900?logo=amazonaws&logoColor=white)
![Nginx](https://img.shields.io/badge/Nginx-Reverse%20Proxy-009639?logo=nginx&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)
![React](https://img.shields.io/badge/React-18-61DAFB?logo=react&logoColor=black)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-14-4169E1?logo=postgresql&logoColor=white)

A production-style DevOps implementation of an existing three-tier e-commerce application using **Docker, Docker Compose, Nginx, PostgreSQL, Git, Linux, and AWS EC2**.

The base application consists of:

- React + TypeScript frontend
- ASP.NET Core 8 backend
- PostgreSQL database

The focus of this project was not to build the application from scratch. The objective was to take an existing three-tier application, understand its architecture, containerize each required layer, orchestrate the services, configure networking and persistence, optimize the images, and deploy the complete application on AWS EC2.

---

# 📌 Project Objective

The project was completed as a practical DevOps implementation covering:

- Existing application analysis
- Local application validation
- Three-tier system design
- Git branching and incremental commits
- Separate frontend and backend Dockerfiles
- Multi-stage Docker builds
- Docker build optimization
- `.dockerignore` configuration
- Non-root backend container execution
- Nginx production web server
- Nginx reverse proxy
- PostgreSQL containerization
- Docker Compose orchestration
- Custom Docker networking
- Docker service-name DNS
- Persistent PostgreSQL storage
- EF Core migrations
- Container health checks
- Runtime environment configuration
- Secrets separation using `.env`
- AWS EC2 deployment
- Security Group configuration
- Public vs internal service exposure
- API validation
- Persistence validation
- Deployment evidence and documentation

---

# 🏗️ System Architecture

The application follows a three-tier architecture.

![System Design](documentation/devops/screenshots/system-design.png)

```text
                         Internet / Browser
                                |
                                | HTTP :80
                                v
                    +-----------------------+
                    |     AWS EC2 Ubuntu    |
                    |                       |
                    |  +-----------------+  |
                    |  |    Frontend     |  |
                    |  | React + Nginx   |  |
                    |  |    Port 80      |  |
                    |  +--------+--------+  |
                    |           |           |
                    |           | /api      |
                    |           v           |
                    |  +-----------------+  |
                    |  |     Backend     |  |
                    |  | ASP.NET Core 8  |  |
                    |  | Internal :8080  |  |
                    |  +--------+--------+  |
                    |           |           |
                    |     EF Core/Npgsql    |
                    |           |           |
                    |           v           |
                    |  +-----------------+  |
                    |  |   PostgreSQL    |  |
                    |  | Internal :5432  |  |
                    |  +--------+--------+  |
                    |           |           |
                    |           v           |
                    |    postgres_data      |
                    |    Named Volume       |
                    +-----------------------+
```

The frontend is the only application service exposed publicly.

The backend and PostgreSQL database remain internal to the Docker network.

---

# 🔄 Request Flow

A normal request follows this path:

```text
User Browser
     |
     | HTTP :80
     v
Nginx / React Frontend
     |
     | /api/v1/...
     v
ASP.NET Core Backend
     |
     | Entity Framework Core
     v
PostgreSQL Database
```

For example:

```text
Browser
   |
   | GET /api/v1/products
   v
Nginx
   |
   | backend:8080
   v
ASP.NET Core
   |
   | db:5432
   v
PostgreSQL
```

This avoids exposing the backend directly to the public internet.

---

# 🧰 Technology Stack

## Frontend

- React 18
- TypeScript
- Vite
- Redux Toolkit
- Material UI
- Tailwind CSS
- Axios
- Nginx

## Backend

- C#
- ASP.NET Core 8
- Entity Framework Core 8
- REST APIs
- JWT Authentication
- Npgsql

## Database

- PostgreSQL 14

## DevOps / Infrastructure

- Git
- GitHub
- Docker
- Docker Compose
- Multi-stage Docker builds
- Docker custom networks
- Docker named volumes
- Container health checks
- Nginx reverse proxy
- Linux
- AWS EC2
- Ubuntu Server

---

# 📁 Repository Structure

![GitHub Repository](documentation/devops/screenshots/github-repository.png)

```text
fullstack-ecommerce-app/
│
├── backend/
│   ├── Ecommerce.Domain/
│   ├── Ecommerce.Infrastructure/
│   ├── Ecommerce.Presentation/
│   ├── Ecommerce.Service/
│   ├── Ecommerce.Tests/
│   ├── Dockerfile
│   └── .dockerignore
│
├── frontend/
│   ├── src/
│   ├── public/
│   ├── Dockerfile
│   ├── .dockerignore
│   └── nginx.conf
│
├── documentation/
│   └── devops/
│       ├── system-design.png
│       └── screenshots/
│
├── docker-compose.yml
├── .gitignore
├── README.md
└── LICENSE
```

---

# 🌿 Git Workflow

A dedicated branch was used for the DevOps implementation:

```text
devops-implementation
```

Changes were committed incrementally instead of making one final bulk commit.

![Git Commit History](documentation/devops/screenshots/git-commit-history.png)

Examples from the project history include:

```text
docs: add three-tier system architecture diagram

chore: ignore macOS metadata files

docker: add multi-stage backend container

fix: prepare frontend for production build

docker: add React frontend with Nginx

db: track and apply EF Core migrations

docker: orchestrate three-tier stack with Compose

merge: complete DevOps implementation
```

This provides traceability and shows the implementation sequence clearly.

The completed DevOps branch was later merged into `main`.

---

# 🐳 Backend Dockerization

The backend uses a multi-stage Docker build.

![Backend Dockerfile](documentation/devops/screenshots/backend-dockerfile.png)

## Stage 1 — Build

The build stage uses the .NET 8 SDK.

It performs:

```text
Copy project files
        |
        v
Restore dependencies
        |
        v
Copy source code
        |
        v
Publish application
```

The `.csproj` files are copied before the full source code so Docker can cache the dependency restore layer.

This means source-code changes do not always require dependencies to be downloaded again.

## Stage 2 — Runtime

The final backend image uses the ASP.NET Core runtime image rather than the complete SDK.

Benefits:

- SDK is not included in production
- fewer unnecessary tools
- smaller runtime image
- reduced attack surface
- cleaner separation between build and runtime stages

The backend is also configured to run using a non-root user.

The runtime user was verified using:

```bash
docker run --rm --entrypoint id ecommerce-backend:local
```

The container returned:

```text
uid=1654(app)
gid=1654(app)
```

This confirms the backend is not running as root.

---

# ⚛️ Frontend Dockerization

The frontend also uses a multi-stage Docker build.

![Frontend Dockerfile](documentation/devops/screenshots/frontend-dockerfile.png)

## Stage 1 — Build

The Node.js build stage performs:

```text
Copy package files
        |
        v
Install dependencies
        |
        v
Copy source code
        |
        v
npm run build
        |
        v
Generate /dist
```

## Stage 2 — Runtime

The generated production files are copied into an Nginx Alpine container.

Final flow:

```text
React + TypeScript
        |
        v
Node.js Build Stage
        |
        v
Vite Production Build
        |
        v
dist/
        |
        v
Nginx Runtime Container
```

Node.js, npm, TypeScript compiler and frontend source code are not required in the final runtime container.

---

# 🌐 Nginx Reverse Proxy

Nginx has two responsibilities:

1. Serve the production React application
2. Proxy API requests to the backend container

Example flow:

```text
Browser
   |
   | /api/v1/products
   v
Nginx
   |
   | backend:8080
   v
ASP.NET Core API
```

This design provides a single public entry point.

The browser does not need direct access to backend port `8080`.

---

# 🐳 Docker Compose

The complete application is orchestrated using Docker Compose.

![Docker Compose](documentation/devops/screenshots/docker-compose.png)

The stack contains three services:

```text
frontend
backend
db
```

Start the complete stack:

```bash
docker compose up -d --build
```

Check service status:

```bash
docker compose ps
```

Stop the stack:

```bash
docker compose down
```

Docker Compose manages:

- service creation
- container networking
- environment variables
- health checks
- persistent storage
- service dependencies

---

# 🔗 Docker Networking

A custom Docker bridge network is used.

![Custom Docker Network](documentation/devops/screenshots/custom-docker-network.png)

Network:

```text
fullstack-ecommerce-app_ecommerce-network
```

Containers communicate using Docker service names instead of container IP addresses.

Frontend to backend:

```text
backend:8080
```

Backend to PostgreSQL:

```text
db:5432
```

This is possible because Docker Compose provides internal DNS resolution.

---

# 🧠 Docker Networking Concept

An important concept demonstrated in this project is that:

```text
localhost
```

inside a container refers to that same container.

Therefore:

```text
Host=localhost
```

would not correctly connect the backend container to the PostgreSQL container.

The Compose environment instead uses:

```text
Host=db
```

where `db` is the PostgreSQL Compose service name.

---

# 🔐 Public vs Internal Ports

| Service | Container Port | Publicly Exposed |
|---|---:|---|
| Frontend / Nginx | 80 | Yes |
| ASP.NET Core Backend | 8080 | No |
| PostgreSQL | 5432 | No |

Only the frontend port is published.

Backend:

```text
8080/tcp
```

remains internal.

PostgreSQL:

```text
5432/tcp
```

also remains internal.

This reduces unnecessary public exposure.

---

# 💾 PostgreSQL Persistent Storage

PostgreSQL data is stored using a Docker named volume.

![PostgreSQL Volume](documentation/devops/screenshots/postgres-volume.png)

The named volume is:

```text
fullstack-ecommerce-app_postgres_data
```

The volume separates database storage from the lifecycle of the PostgreSQL container.

Without a volume:

```text
Delete DB container
        |
        v
Database data may be lost
```

With the named volume:

```text
Delete DB container
        |
        v
Volume remains
        |
        v
Create new DB container
        |
        v
Existing database data restored
```

---

# 🔄 Persistence Validation

Persistence was tested practically.

## Before Container Recreation

A test record was inserted into PostgreSQL.

![Persistence Before](documentation/devops/screenshots/persistence-before.png)

The record was verified before removing the containers.

The containers were then stopped and removed using:

```bash
docker compose down
```

The command intentionally did **not** use:

```bash
docker compose down -v
```

because `-v` would remove the named volume.

The stack was recreated using:

```bash
docker compose up -d
```

## After Container Recreation

The same database record was queried again.

![Persistence After](documentation/devops/screenshots/persistence-after.png)

The record still existed after container recreation.

This proved that PostgreSQL data was stored outside the container lifecycle.

---

# 🗄️ Entity Framework Core Migrations

EF Core migrations are tracked in Git.

The migration files define the database schema required by the application.

Pending migrations are automatically applied during backend startup:

```csharp
using (var scope = app.Services.CreateScope())
{
    var dbContext =
        scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    dbContext.Database.Migrate();
}
```

This makes the database setup reproducible.

When a fresh PostgreSQL container starts:

```text
PostgreSQL
     |
     v
Backend starts
     |
     v
EF Core checks migrations
     |
     v
Pending migrations applied
     |
     v
Required schema available
```

---

# ❤️ Container Health Checks

Health checks were configured for important services.

## PostgreSQL

PostgreSQL readiness is validated using:

```text
pg_isready
```

## Backend

The backend has its own health validation.

The final running services showed:

```text
ecommerce-db         healthy
ecommerce-backend    healthy
ecommerce-frontend   running
```

Health checks are useful because a container being in the `running` state does not always mean the application inside it is actually ready.

---

# 📦 Docker Image Optimization

Both the frontend and backend use multi-stage Docker builds.

![Docker Images](documentation/devops/screenshots/docker-images.png)

Final local Docker Desktop image sizes:

```text
Backend   : 203.77 MB
Frontend  : 107.12 MB
```

The EC2 deployment was built directly on the target Linux host, allowing Docker to build images for the EC2 architecture.

## Optimization Choices

The following optimization practices were used:

### Multi-stage Builds

Build tools are separated from runtime containers.

Backend:

```text
.NET SDK
    |
    v
Publish
    |
    v
ASP.NET Runtime
```

Frontend:

```text
Node.js
    |
    v
Vite Build
    |
    v
Nginx
```

### Smaller Runtime Images

Runtime containers contain only what is required to execute the application.

### Docker Layer Caching

Dependency files are copied before application source files.

Backend example concept:

```text
COPY *.csproj
RUN dotnet restore
COPY source
```

Frontend concept:

```text
COPY package*.json
RUN npm install
COPY source
```

This allows dependency layers to be reused when source code changes.

### `.dockerignore`

Files unnecessary for the build context are excluded.

Examples include:

```text
node_modules
bin
obj
.git
local build artifacts
```

This reduces Docker build context size.

### Non-root Backend Execution

The backend runs using an application user instead of root.

This reduces the privilege level of the running application.

---

# 🔑 Runtime Configuration and Secrets

Environment-specific values are not hardcoded into Docker images.

Configuration is injected at runtime.

Examples:

```env
POSTGRES_DB=ecommerce_db
POSTGRES_USER=ecommerce_user
POSTGRES_PASSWORD=your_database_password

JWT_ISSUER=EcommerceAPI
JWT_KEY=your_jwt_signing_key
```

The real `.env` file must not be committed.

Sensitive files that must remain outside Git include:

```text
.env
*.pem
database passwords
JWT signing keys
GitHub tokens
AWS credentials
```

---

# 🖥️ Local Validation

Before deploying to AWS, the application was tested locally.

The local stack was validated at each layer:

```text
React frontend
      |
      v
ASP.NET Core backend
      |
      v
PostgreSQL
```

The backend API was tested separately.

The frontend was then verified to load product data successfully.

This step was important because it separated application-level problems from Docker and cloud-deployment problems.

---

# ☁️ AWS EC2 Deployment

The complete Docker Compose stack was deployed on an Ubuntu AWS EC2 instance.

![EC2 Instance Running](documentation/devops/screenshots/ec2-instance-running.png)

Deployment flow:

```text
Development on Mac
        |
        v
Git Commit
        |
        v
GitHub Repository
        |
        v
Clone Repository on EC2
        |
        v
Create Runtime .env
        |
        v
Docker Compose Build
        |
        v
Docker Compose Up
        |
        v
Frontend + Backend + PostgreSQL
```

Docker Engine and Docker Compose were installed on the EC2 instance.

The repository was cloned from GitHub and the stack was built directly on EC2.

Command:

```bash
docker compose up -d --build
```

---

# 🛡️ AWS Security Group

The EC2 Security Group controls inbound access.

![Security Group](documentation/devops/screenshots/security-group.png)

Required inbound access:

```text
SSH     TCP 22    Restricted source IP
HTTP    TCP 80    0.0.0.0/0
```

Ports intentionally not exposed publicly:

```text
8080
5432
```

Therefore:

```text
Internet
   |
   v
Port 80
   |
   v
Nginx
```

while backend and database remain internal.

---

# ✅ Docker Compose Running on EC2

The complete three-tier stack was successfully started on EC2.

![EC2 Docker Compose](documentation/devops/screenshots/ec2-compose-running.png)

Final service state:

```text
ecommerce-backend    healthy
ecommerce-db         healthy
ecommerce-frontend   running
```

Only the frontend container publishes a host port:

```text
0.0.0.0:80 -> 80
```

Backend:

```text
8080/tcp
```

Database:

```text
5432/tcp
```

remain internal.

---

# 🔌 API Validation on EC2

The deployed API was tested directly from the EC2 instance.

Command:

```bash
curl "http://localhost/api/v1/products?limit=5"
```

![EC2 API Test](documentation/devops/screenshots/ec2-api-test.png)

The response successfully returned product data including items such as:

```text
Smartphone ABC
Laptop XYZ
TV DEF
Headphones JKL
Tablet GHI
```

This confirmed the complete request path:

```text
Nginx
  |
  v
ASP.NET Core
  |
  v
Entity Framework Core
  |
  v
PostgreSQL
```

---

# 🌍 Live Application on AWS EC2

The final application was accessed using the EC2 public IPv4 address.

![Live EC2 Website](documentation/devops/screenshots/ec2-live-website.png)

The working frontend confirmed:

- Nginx serving production React files
- public HTTP connectivity
- Nginx API reverse proxy
- backend connectivity
- database connectivity
- successful complete deployment

---

# 🧯 Troubleshooting and Technical Learnings

Only meaningful technical issues encountered during implementation are documented here.

## 1. Full .NET Solution Build vs Deployable Backend

The complete solution initially failed because the existing test project was not aligned with the current application service constructor.

The deployable backend projects were isolated and built separately.

### Learning

A solution-level build failure does not automatically mean the production application cannot build.

The correct approach is to identify which project or layer is failing before changing production code.

---

## 2. PostgreSQL Role / Authentication Failure

During database setup, EF Core database update returned an error indicating that:

```text
role "ecommerce_user" does not exist
```

The PostgreSQL server itself was reachable, but the application database identity had not been created.

A dedicated application role was created and assigned to the database.

### Learning

Database connectivity consists of multiple layers:

```text
Network reachable
        +
Database exists
        +
User/role exists
        +
Credentials correct
        +
Permissions correct
```

A successful network connection does not automatically mean authentication is configured correctly.

---

## 3. npm Peer Dependency Conflict

The existing frontend dependencies produced an npm peer-dependency resolution conflict.

The project uses React 18 while one transitive dependency attempted to resolve a newer incompatible router dependency.

The baseline project installation was completed using:

```bash
npm install --legacy-peer-deps
```

### Learning

Dependency conflicts should be understood before using destructive options such as:

```text
--force
```

For an existing application, preserving a known working dependency baseline is important before making infrastructure changes.

---

## 4. Frontend Development Mode vs Production Build

The React application could run through the Vite development server, but the first production build failed during:

```bash
npm run build
```

TypeScript production checks exposed source-code issues that development mode had not prevented.

The relevant issues were corrected before the Docker production image was rebuilt.

### Learning

```text
npm run dev succeeds
```

does not necessarily mean:

```text
npm run build succeeds
```

A production Dockerfile must validate the actual production build pipeline.

---

## 5. Container Networking Between Host and Docker

During standalone backend-container testing, PostgreSQL was still running directly on the Mac.

The backend container therefore used:

```text
host.docker.internal
```

to reach the host machine.

After PostgreSQL was moved into Docker Compose, the connection changed to:

```text
Host=db
```

### Learning

There are three different networking contexts:

```text
localhost
```

means the current machine/container.

```text
host.docker.internal
```

allows a Docker Desktop container to reach the host machine.

```text
db
```

is Docker Compose DNS for the database service.

Understanding this distinction is essential when moving an application from local execution into containers.

---

## 6. EC2 SSH Connectivity and Security Group Source IP

SSH connectivity to EC2 initially timed out even though the EC2 instance itself was running.

The problem was related to the inbound SSH source configuration in the Security Group.

After validating the current public client IP and updating the inbound rule, SSH connectivity was restored.

### Learning

When SSH times out, troubleshoot the network path before assuming the SSH key is invalid.

Important checks include:

```text
Instance state
Public IPv4
Security Group
Port 22
Source IP
Subnet / route connectivity
SSH username
Key permissions
```

---

# 🎯 Final Result

The completed project demonstrates:

- Existing three-tier application analysis
- Local application validation
- Three-tier system design
- Git branching
- Meaningful incremental commits
- React frontend containerization
- ASP.NET Core backend containerization
- PostgreSQL containerization
- Separate frontend and backend Dockerfiles
- Multi-stage Docker builds
- Docker layer optimization
- `.dockerignore`
- Non-root backend execution
- Nginx production serving
- Nginx reverse proxy
- Docker Compose orchestration
- Custom Docker bridge network
- Docker internal DNS
- Service-name communication
- Internal backend port
- Internal PostgreSQL port
- PostgreSQL named volume
- Database persistence validation
- EF Core migrations
- Automatic migration execution
- Container health checks
- Runtime secrets
- AWS EC2 deployment
- Security Group configuration
- Public frontend access
- API validation
- Complete deployment evidence

---

# 📸 Deployment Evidence

The complete deployment evidence is available in:

```text
documentation/devops/screenshots/
```

The evidence includes:

```text
System design
GitHub repository
Git commit history
Backend Dockerfile
Frontend Dockerfile
Docker Compose configuration
Docker image sizes
Custom Docker network
PostgreSQL named volume
Persistence before recreation
Persistence after recreation
EC2 instance
Security Group
Docker Compose running on EC2
API test
Live EC2 application
```

A separate PDF / document is also prepared for the final practical submission containing the deployment screenshots in sequence.

---

# 🚀 Future Improvements

Possible next stages for this project include:

- GitHub Actions CI/CD pipeline
- Automated test pipeline
- Automated Docker builds
- Amazon ECR
- Amazon RDS
- HTTPS/TLS
- Domain name configuration
- AWS Application Load Balancer
- Terraform
- Prometheus monitoring
- Grafana dashboards
- Centralized logging
- Docker image vulnerability scanning
- Automated PostgreSQL backups
- Blue/green or rolling deployments

---

# 🙏 Original Application Credit

The base application was forked from the open-source repository:

**MohamadNach/fullstack-ecommerce-app**

The original project provided the React, ASP.NET Core and PostgreSQL application source.

The DevOps implementation added in this repository includes:

- system architecture
- Git implementation history
- frontend Dockerfile
- backend Dockerfile
- multi-stage builds
- `.dockerignore`
- Nginx configuration
- Docker Compose
- Docker custom networking
- PostgreSQL persistent storage
- runtime environment configuration
- container health checks
- EF Core migration handling
- AWS EC2 deployment
- Security Group configuration
- deployment validation
- troubleshooting documentation
- deployment evidence
