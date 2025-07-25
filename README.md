# 📦 foodDelivery

**foodDelivery** is a .NET-based backend system for an online food delivery platform. It allows **customers** to browse restaurants and menus, place orders, and manage profiles. **Vendors** can manage their restaurants, menus, and orders. **Admins** can monitor and manage reports. The project follows a clean architecture pattern with clearly separated responsibilities.

---

## 🚀 Tech Stack

- **ASP.NET Core**
- **Entity Framework Core**
- **JWT Authentication**
- **MSSQL Server**
- **Clean Architecture**
- **RESTful APIs**

---

## 🧱 Project Structure

This solution is organized in a layered architecture:

- `foodDelivery.Domain`: Contains core entities and enums.
- `foodDelivery.Application`: Contains DTOs, interfaces, and use case logic.
- `foodDelivery.Infrastructure`: Implements services, DB access (DbContext), and external integrations.
- `foodDelivery.API`: The main API project with controllers and middleware.

---

## 🌿 Git Branch Strategy

This project follows the **Git Flow** branching model:

- `main`: Stable production-ready code.
- `develop`: Development integration branch.
- `feature/*`: New features in progress.
- `refactor/*`: Project restructuring and code improvements.
- `init/*`: Initial project setup.

---

## 🔒 Authentication

Authentication is handled via **JWT tokens**, and token revocation is supported for secure logout. Claims include `UserId` and `Role`, and each API checks access permissions based on role.
