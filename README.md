# Vehicle Speed Configuration Platform

A full-stack web application built for a real client to centralize and audit the configuration of vehicle speed-limiting devices across a managed fleet. Replaces a fully manual, untraceable process with a secure, web-based interface that enables remote configuration, role-based access control, and complete audit traceability.

---

## Problem It Solves

Before this system, technicians had to physically connect to each speed-limiting device installed in a vehicle and configure it manually — one by one, with no record of who changed what or when. For a fleet with dozens of vehicles, this was slow, error-prone, and completely unauditable.

This platform replaced that process with a centralized web interface where authorized users can:
- Configure any device in the fleet remotely via USB connection
- See the full history of every configuration change
- Manage clients and their associated vehicle fleets
- Control who has access to which functions through role-based permissions

---

## Key Features

### 🔐 Authentication & Role-Based Access Control
- Secure login with session management
- User roles with differentiated permissions — not every user can access every function
- Credential protection and session expiration

### ⚙️ Real-Time Device Configuration
- Web interface for setting speed parameters per device
- USB integration for direct communication with the physical hardware installed in vehicles
- Changes applied in real time upon device connection

### 📋 Audit Logging & Traceability
- Every configuration change is logged: **who** made it, **when**, and **what changed** (previous value → new value)
- Complete configuration history per device and per vehicle
- Full accountability across the fleet

### 🚗 Fleet & Client Management
- Register and manage clients and their associated vehicles
- View current configuration status per fleet
- Associate devices to specific vehicles and clients

---

## Tech Stack

| Layer | Technology |
|---|---|
| Backend | C# / ASP.NET MVC (.NET Framework) |
| Architecture | MVC (Model-View-Controller) |
| Frontend | HTML / CSS / JavaScript (Razor Views) |
| Database | MySQL |
| Device Integration | USB Communication |
| Authentication | Custom auth with session management |
| Version Control | Git |

---

## Architecture Overview

```
┌─────────────────────────────────────────┐
│              Web Browser                │
│         (HTML/CSS/JS Razor Views)       │
└────────────────┬────────────────────────┘
                 │ HTTP Requests
┌────────────────▼────────────────────────┐
│         ASP.NET MVC (.NET Framework)    │
│    Controllers → Models → Views         │
│    App_Start: RouteConfig, BundleConfig │
└──────────┬──────────────────────────────┘
           │                    │
┌──────────▼──────┐   ┌─────────▼─────────┐
│   MySQL DB      │   │  USB Device Layer  │
│ - Users/Roles   │   │  (Serial Comm.)    │
│ - Vehicles      │   │  Speed Limiters    │
│ - Audit Logs    │   └────────────────────┘
│ - Configs       │
└─────────────────┘
```

---

## Project Background

- **Duration:** 12 months (full lifecycle)
- **Client:** Real external client (transportation/fleet management)
- **Role:** Technical Project Lead — responsible for requirements gathering, architecture design, full-stack development, client communication, and formal delivery sign-off
- **Outcome:** Delivered and accepted by client; replaced a fully manual process with zero auditability

---

## What This Project Demonstrates

- **Full-stack development** in C# / ASP.NET MVC from scratch
- **Relational database design** with MySQL (multi-tenant schema for clients, vehicles, devices, audit logs)
- **Authentication and access control** implementation
- **Hardware integration** (USB communication with physical devices)
- **Audit and compliance** design patterns (immutable change history)
- **Project ownership** — requirements through delivery with a real client
- **Stakeholder communication** and formal sign-off process

---

## Getting Started

### Prerequisites

- [Visual Studio](https://visualstudio.microsoft.com/) (2019 or later recommended) with ASP.NET workload
- [MySQL Server](https://dev.mysql.com/downloads/mysql/)
- A compatible USB speed-limiting device (for full hardware functionality)

### Setup

```bash
# Clone the repository
git clone https://github.com/ODR3N/Vehicle-Speed-Limiter-Configuration-Platform.git
```

1. Open `ModuloLimitadorVelocidad.sln` in Visual Studio
2. Update the MySQL connection string in `Web.config`:
```xml
<connectionStrings>
  <add name="DefaultConnection"
       connectionString="server=localhost;database=speedlimiter;uid=root;pwd=yourpassword;"
       providerName="MySql.Data.MySqlClient" />
</connectionStrings>
```
3. Restore NuGet packages (Visual Studio will prompt automatically)
4. Run the database schema scripts from the `App_Data` folder
5. Press **F5** or click **Run** in Visual Studio

---

## Project Structure

```
ModuloLimitadorVelocidad/
├── PModuloLimitadorV/
│   ├── App_Start/          # Route, bundle, and auth configuration
│   ├── Controllers/        # MVC Controllers (Auth, Fleet, Config, Audit)
│   ├── Models/             # Data models and ViewModels
│   ├── Views/              # Razor views (.cshtml)
│   ├── Content/            # CSS stylesheets
│   ├── Scripts/            # JavaScript files
│   ├── App_Data/           # Database schema and seed scripts
│   ├── images/             # Static image assets
│   ├── Global.asax         # Application lifecycle events
│   ├── Startup.cs          # OWIN startup configuration
│   └── Web.config          # App configuration and connection strings
├── packages/               # NuGet dependencies
└── ModuloLimitadorVelocidad.sln
```

---

## Author

**Adrian Fonseca**
- GitHub: [@ODR3N](https://github.com/ODR3N)
- LinkedIn: [linkedin.com/in/afc2806](https://www.linkedin.com/in/afc2806)
- Portfolio: [odr3n.github.io](https://odr3n.github.io)

---

*Delivered to a real client. Not a tutorial project.*
