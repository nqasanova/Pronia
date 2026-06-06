# Pronia - Plant E-Commerce Web Application

A full-featured e-commerce platform for plants and botanical products, built with ASP.NET Core 6 MVC and SQL Server.

---

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [Configuration](#configuration)
- [Database Setup](#database-setup)
- [Architecture](#architecture)
- [Admin Panel](#admin-panel)
- [Client Area](#client-area)

---

## Overview

Pronia is a multi-area ASP.NET Core MVC web application that provides a complete shopping experience for plant and botanical products. It ships with a customer-facing storefront and a full admin panel for managing every aspect of the store — products, orders, blogs, sliders, users, and more.

---

## Features

### Customer-Facing Storefront
- **Product Catalog** — Browse products with filtering by category, color, size, and tag
- **Product Details** — Image gallery, variant selection (color, size), and rich description
- **Shopping Basket** — Cookie-based cart for guests, database-persisted cart for logged-in users
- **Checkout & Orders** — Order placement with unique tracking codes (e.g. `OR12345`)
- **User Accounts** — Registration with email confirmation, login/logout, account dashboard
- **Blog** — Articles with categories, tags, file attachments, and related post sidebar
- **About Page** — Team info, company stats, and mission content managed via the admin panel
- **Contact Form** — Customer inquiries stored and reviewable in the admin panel
- **Testimonials & Sliders** — Hero banners and customer quotes managed dynamically

### Admin Panel
- **Dashboard** — Overview of store activity
- **Product Management** — Full CRUD for products including multi-image upload, categories, colors, sizes, and tags
- **Order Management** — View and update order statuses
- **Blog Management** — Create, edit, and delete blog posts with category/tag assignment and file attachments
- **User & Role Management** — Create users, assign roles
- **Content Management** — Manage sliders, navbar links, sub-navbar items, payment benefits, feedback, and about content
- **Contact Inbox** — Review all customer contact form submissions

---

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core 6 MVC |
| ORM | Entity Framework Core 7 |
| Database | SQL Server (MSSQL) |
| Authentication | Custom cookie-based auth with BCrypt password hashing |
| Email | MailKit (SMTP via Gmail) |
| Validation | FluentValidation |
| Admin UI | Custom admin theme with ApexCharts, Leaflet, DataTables |
| Client UI | Custom storefront theme with product sliders and cart UI |

### NuGet Packages

```
Microsoft.EntityFrameworkCore 7.0.2
Microsoft.EntityFrameworkCore.SqlServer 7.0.2
FluentValidation.AspNetCore 11.2.2
MailKit 3.4.3
BCrypt.Net-Core 1.6.0
Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation 6.0.11
AspNetCore.IServiceCollection.AddIUrlHelper 1.1.0
```

---

## Project Structure

```
Pronia/
└── Pronia/
    └── Pronia/
        ├── Areas/
        │   ├── Admin/
        │   │   ├── Controllers/       # Admin controllers (Product, Blog, Order, User, etc.)
        │   │   ├── ViewModels/        # Admin-specific view models
        │   │   ├── Validators/        # FluentValidation validators for admin forms
        │   │   ├── ViewComponents/    # Navbar header and footer components
        │   │   └── Views/             # Razor views for admin UI
        │   └── Client/
        │       ├── Controllers/       # Storefront controllers (Home, Shop, Basket, Checkout, etc.)
        │       ├── ViewModels/        # Client-facing view models
        │       ├── ViewComponents/    # Cart, product, navbar, filter components
        │       └── Views/             # Razor views for storefront UI
        ├── Database/
        │   ├── Models/                # Entity models (Product, Order, User, Blog, etc.)
        │   │   └── Common/            # BaseEntity<T> and IAuditable interface
        │   ├── Configurations/        # EF Core Fluent API configurations per entity
        │   └── Datacontext.cs         # DbContext with all DbSets and auditing hooks
        ├── Services/
        │   ├── Abstracts/             # Service interfaces (IBasketService, IEmailService, etc.)
        │   └── Concretes/             # Service implementations
        ├── Infastructure/
        │   ├── Configurations/        # DI registration, MVC, EF, FluentValidation setup
        │   └── Extentions/            # IServiceCollection and IApplicationBuilder extensions
        ├── Migrations/                # EF Core migration history
        ├── Options/                   # Strongly-typed config options (EmailConfigOptions)
        ├── Validators/                # Shared validators (e.g. file upload validator)
        ├── Exceptions/                # Custom exception types
        ├── Extensions/                # ModelBuilder and HttpResponse extensions
        ├── Contracts/                 # DTO records (Email MessageDto, File contracts)
        ├── wwwroot/
        │   ├── admin/                 # Admin panel static assets
        │   └── client/                # Storefront static assets (images, CSS, JS)
        ├── appsettings.json
        └── Program.cs
```

---

## Getting Started

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or SQL Server Express / LocalDB)
- A Gmail account (for email confirmation functionality)

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd Pronia/Pronia
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Configure the application** — see [Configuration](#configuration) below.

4. **Apply database migrations**
   ```bash
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   dotnet run --project Pronia
   ```

6. Open your browser and navigate to `https://localhost:5001` (or the port shown in the console).

---

## Database Setup

The project uses **Entity Framework Core Code-First** migrations. The initial schema and all subsequent changes are tracked in the `Migrations/` folder.

To apply all migrations and create the database:

```bash
cd Pronia
dotnet ef database update
```

To add a new migration after changing a model:

```bash
dotnet ef migrations add MigrationName
dotnet ef database update
```

### Data Models Overview

| Model | Description |
|---|---|
| `Product` | Core product with name, price, content, and relations to images, colors, sizes, tags, and categories |
| `Order` | Customer order with tracking code (e.g. `OR12345`), status enum, and total |
| `Basket` / `BasketProduct` | Per-user basket with line items; guests use cookies, logged-in users use the DB |
| `User` | Email/password user with role, email confirmation, and activation token |
| `Blog` | Blog post with categories, tags, and file attachments |
| `Slider` | Hero banner slides managed from the admin panel |
| `Navbar` / `SubNavbar` | Dynamic navigation structure |
| `PaymentBenefit` | Configurable shipping/payment feature highlights |
| `Feedback` | Customer testimonials |
| `Contact` | Contact form submissions |
| `About` | About page content (editable from admin) |

All entities inherit from `BaseEntity<TId>` and implement `IAuditable` (`CreatedAt`, `UpdatedAt`), which are automatically stamped via a `SaveChanges` override in `DataContext`.

---

## Architecture

### Multi-Area MVC

The application is split into two ASP.NET Core Areas:

- **`Admin`** — Protected area for store management. Route prefix: `/admin`
- **`Client`** — Public storefront. Route prefix: `/` (default)

### Service Layer

Business logic is decoupled from controllers via a service abstraction layer:

| Interface | Implementation | Responsibility |
|---|---|---|
| `IUserService` | `UserService` | Auth, registration, sign-in/out, password check |
| `IBasketService` | `BasketService` | Add/remove/sync basket items (cookie ↔ DB) |
| `IOrderService` | `OrderService` | Generate unique order tracking codes |
| `IEmailService` | `SMTPService` | Send transactional emails via Gmail SMTP |
| `IFileService` | `FileService` | File upload handling and path management |
| `IUserActivationService` | `UserActivationService` | Email activation token creation and validation |

Services are registered in `RegisterCustomServicesConfigurations.cs` and injected throughout via constructor injection.

### Basket Strategy

The basket uses a dual-mode strategy:
- **Guest users** — basket stored as a JSON-serialized cookie
- **Authenticated users** — basket persisted in the `Baskets` / `BasketProducts` tables
- On login, the cookie basket is merged into the database basket automatically

### Email Confirmation

After registration, users receive a time-limited activation link (configurable via `ActivationValidityMinute` in `appsettings.json`, default 5 minutes). The token is stored in the `UserActivations` table and validated on the activation route.

---

## Admin Panel

Access the admin panel at `/admin`. Admin accounts are created directly in the database or via the User Management section.

**Admin modules:**

- **Dashboard** — Store summary
- **Products** — CRUD with image gallery management and variant assignment
- **Orders** — View all orders, update statuses
- **Categories / Colors / Sizes / Tags** — Product attribute management
- **Blog** — Post editor with category, tag, and file attachment support
- **Sliders** — Hero banner management
- **Navbar / Sub-Navbar** — Dynamic navigation builder
- **Payment Benefits** — Feature highlight cards (free shipping, returns, etc.)
- **Feedback** — Customer testimonials
- **Contact** — Inbox for contact form submissions
- **Users** — User listing and role assignment
- **About** — Edit about page content

---

## Client Area

The storefront is accessible at `/` and includes:

- **Home** (`/`) — Hero slider, featured products, blog previews, testimonials
- **Shop** (`/shop`) — Full product catalog with sidebar filters (category, color, size, tag, price)
- **Product Detail** (`/product/{id}`) — Image gallery, variant picker, add to basket
- **Cart** (`/cart`) — Basket summary with quantity controls
- **Checkout** (`/checkout`) — Order placement (requires login)
- **Blog** (`/blog`) — Article listing with category and tag sidebar
- **Blog Detail** (`/blog/{id}`) — Full article with related posts
- **About** (`/about`) — Company info, team, stats
- **Contact** (`/contact`) — Contact form
- **Account** (`/account`) — User dashboard, order history (requires login)
- **Auth** (`/auth/login`, `/auth/register`) — Login and registration

---
