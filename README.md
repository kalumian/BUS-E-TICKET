# Bus E-Ticket Platform

## 1. Problem Description and Project Objective
The intercity public transport sector faces major operational challenges due to the lack of digital infrastructure among many companies. This results in inefficient booking management, difficulty in reaching customers, and complicated payment processes. Additionally, the absence of integration among service providers makes the booking experience cumbersome and ineffective for customers. Moreover, small and emerging companies struggle with digital transformation due to high costs and a lack of technical expertise. These gaps lead to reduced operational efficiency and hinder growth and expansion opportunities in the sector.

## 2. Proposed Solution
**Bus E-Ticket Platform** is a fully integrated digital system that connects intercity transport companies with customers through a unified electronic booking platform. It provides service providers with a control panel to efficiently manage trips and payments while allowing customers to search for trips, book tickets, and manage their reservations easily.

The system is based on the **SaaS** model, reducing operational costs for emerging companies while supporting electronic payments and digital integration to ensure a seamless and secure experience. By digitizing the sector, the project helps companies improve their operations and offers customers a faster and more convenient booking experience.

## 3. Target Audience
- **Intercity transport companies** that lack adequate digital infrastructure to manage their transport activities.
- **Customers** looking for a fast, comfortable, and secure way to access various intercity transport companies and browse trips with high-quality service.

## 4. System Overview
The platform consists of:
### 4.1 Server
### 4.2 First Interface (B2B) – For Companies and Service Providers
- Provides a **comprehensive control panel** for service providers, allowing them to manage resources, create trips, and handle bookings and customers.
- **Platform administrators** can receive service provider requests, approve or reject them, and activate accounts accordingly.
- **Administrators** have access to all trips, customers, and account management functions.

### 4.3 Second Interface (B2C) – For Customers and Travelers
- All trips added by service providers are displayed on a **separate website** dedicated solely to customers and travelers.
- The website enables users to **search for available trips** from all service providers using an advanced search engine, with the option to **book without creating an account**.
- The platform offers a **smart data retention system**, allowing registered customers to save their identity details for future bookings.
- Customers can **purchase tickets, download invoices, and access booking information easily**.
- **Non-registered users** can retrieve their booking details using the **PNR number search service**.

## 5. Summary of the Control Panel and System Management Interfaces
**Supports multiple languages using Google Translate.**

### 5.1 Login Page
- **Login verification** – Backend and frontend validation; inactive or deleted accounts cannot access the platform.
- **Redirect to service provider registration** request page for new service providers.
- **User redirection after login** to the appropriate interface (Admin or Service Provider).
- **Service Provider Registration Page:**
  - Requires necessary details (company information, location, logo, account details).
  - **Access granted only after admin approval**.
  - Duplicate requests **not allowed** if a previous request is pending.
  - A new request can be submitted if the previous one was **rejected or does not exist**.

### 5.2 Admin Interface
#### 5.2.1 Home Page
- General **statistics and system information**.

#### 5.2.2 Admin List Page
- **List of all system administrators** with summary information.
- Options to **view, edit, or delete** accounts.
- **Delete button** – Admins are **marked as deleted** instead of being removed from the database.

#### 5.2.3 Service Provider Page
- **List of all service providers** with summary information.
- Options to **view, edit, or delete** accounts.
- **Every service provider is linked to a registration request or document**.

#### 5.2.4 Customer Page
- **List of all customers** with summary information.
- Options to **view, edit, or delete** accounts.

#### 5.2.5 Registration Requests Page
- **Displays pending registration requests** from service providers.
- **Approve or reject requests** with a form-based system.
- **Rejected accounts are deleted, approved accounts are activated**.

#### 5.2.6 Trips Page
- Displays **all trips** added by service providers.
- Options to **view bookings** or **trip details**.

#### 5.2.7 Bookings Page
- **Displays all bookings** with summary information.
- Options to **view booking details** or **traveler details**.

#### 5.2.8 Travelers Page
- **List of all travelers**, including registered and unregistered users.
- **View traveler details**.

### 5.3 Customer Interface
#### 5.3.1 Home Page
- Redirects to the **trip search page**.
- **Search Form:** Departure city, destination city, and travel date.

#### 5.3.2 Booking Page
- **Displays trip details**.
- **Auto-fills traveler details** for registered users.
- **Supports electronic payments** (PayPal & Virtual Payment Gateway).
- **Policy confirmation is mandatory** before payment.
- **Booking cancellation is automatic if payment is not completed within 5 minutes**.

#### 5.3.3 Payment Confirmation Pages
- **Success:** Shows invoice & ticket download buttons.
- **Failure:** Redirects the user back to booking.

#### 5.3.4 Additional Features
- **Search for a booking using PNR**.
- **User authentication system** with account creation and history tracking.
- **User Dashboard** displaying past bookings, tickets, and invoices.

## 6. Technical Information of the System
### 6.1 Database
- **PostgreSQL** with **high-level normalization**.

### 6.2 Backend
- **Technology:** .NET Core MVC Web API
- **Architecture:** N-Tier Architecture

#### 6.2.1 Technologies & Libraries Used:
- **Entity Framework** – ORM for database management.
- **JWT** – Authentication and authorization.
- **Custom Middleware** – Global error handling.
- **PayPal API Integration** for secure transactions.

#### 6.2.2 Backend Layers:
- **Data Access Layer (DAL):** Handles database operations.
- **Core Layer:** Shared components and business logic.
- **Business Layer (BLL):** Service patterns & logic.
- **Presentation Layer (PL):** API controllers for frontend communication.

### 6.3 Frontend
- **Technology Used:** React.js + TypeScript
- **Libraries & Tools:**
  - **SASS & Bootstrap** – Modern UI design.
  - **Axios** – API requests.
  - **JWT Token Storage** – Secure authentication.
  - **Route Design Patterns** – Efficient page navigation.

## 7. Project Summary
- 🔹 **Database:** PostgreSQL with normalization.
- 🔹 **Backend:** .NET Core MVC Web API with N-Tier Architecture.
- 🔹 **Security:** JWT authentication & global error handling.
- 🔹 **Payment:** Integrated with **PayPal API**.
- 🔹 **Frontend:** React.js + TypeScript.
- 🔹 **UI/UX:** Built using **SASS & Bootstrap**.
- 🔹 **State Management:** Axios + Route Design Patterns.

# 🚍 Bus E-Ticket Platform

## 📌 Project Overview
Bus E-Ticket Platform is a fully integrated digital system that connects intercity transport companies with customers through a unified electronic booking platform. It provides service providers with a control panel to efficiently manage trips and payments, while allowing customers to search for trips, book tickets, and manage their reservations easily.

## 📌 Problem Description
The intercity public transport sector faces major operational challenges due to the lack of digital infrastructure among many companies. This results in:
- Inefficient **booking management**.
- Difficulty in reaching **customers**.
- **Complicated** payment processes.
- Lack of **integration** among service providers.
- Small and emerging companies struggle with digital transformation due to
## 🚀 Project Setup and Execution

### 🔧 Backend Setup
1. The backend is connected to a **PostgreSQL database** by default.
2. To change the database connection:
   - Navigate to the **configuration file** in the **Core Layer**.
   - Replace the existing **database URL** with your own.
   - Run the following command to apply the database structure:

   ```sh
   dotnet ef database update
If you are using the default database connection, you can log in with the following admin credentials:

Username: Admin0
Password: Kalumian@4002
🎨 Frontend Setup
Navigate to the frontend project directory.

Install dependencies by running:
```sh
npm install
```
Start the frontend server with:

```sh
npm start
```
The application should now be accessible via the local development server.

vbnet
This section is now properly formatted for a **README.md** file. 🚀 Let me know if you need any adjustments!
