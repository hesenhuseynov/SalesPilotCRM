**SalesPilotCRM - Domain Layer Documentation**

---

## Overview

This documentation provides a comprehensive explanation of the **Domain Layer** in the *SalesPilotCRM* project, designed using Clean Architecture principles. It includes detailed descriptions of each Entity, their relationships, business intentions, and the domain logic that guides their structure. The aim is to offer clarity to both current and future developers — especially those unfamiliar with CRM systems — by explaining not just the data model but also the business context and rules behind it.

---

## CRM Business Domain – Strategic Perspective

### What is a CRM System?
A **Customer Relationship Management (CRM)** system is a business tool that enables companies to track, manage, and analyze customer interactions throughout the entire customer lifecycle. The CRM's goal is to improve business relationships, enhance customer retention, and drive sales growth.

### Key Business Goals of SalesPilotCRM:
- 🧑‍💼 **Centralized Customer Management**: Keep all customer information structured and accessible.
- 💼 **Sales Pipeline Visibility**: Allow sales teams to track deals from initial contact to closure.
- 📞 **Interaction History**: Maintain a historical log of communications and actions with each customer.
- ⏰ **Reminder & Follow-ups**: Avoid missing opportunities by tracking next actions.
- 📊 **Performance Metrics**: Evaluate agent performance, conversion ratios, and customer lifecycle stage progression.

> The domain layer reflects these goals by modeling each business concept as an entity, incorporating relationships, traceability (audit), and extensibility.

---

## BaseEntity & Auditing

### `BaseEntity`
```csharp
public abstract class BaseEntity : IAuditable
```
All domain entities inherit from `BaseEntity`, which provides:

- `Id` (int): Primary identifier
- `CreatedAt`, `CreatedBy`: Who and when created the record
- `UpdatedAt`, `UpdatedBy`: Who and when updated it
- `IsDeleted`: Logical deletion support (soft delete)

This ensures consistency and traceability across all entities, supporting business rules like audit compliance, soft deletion (recoverable data), and accountability.

---

## Entities

### 1. **User**
Represents system users such as Sales Agents or Admins.

**Purpose:** Assign responsibility, enable authentication, and track audit history.

**Fields:**
- `FullName`, `Email`
- `PasswordHash`, `PasswordSalt`: Secure authentication storage
- `RoleId`: Foreign key to `Role`
- `IsActive`: Status toggle

**Navigation:**
- `Role`: Linked role
- `AssignedCustomers`, `AssignedDeals`: Deals and customers assigned to the user

---

### 2. **Role**
Represents the access level of a user. Rather than using enums, roles are stored as an entity to allow dynamic control and extensibility.

**Business Purpose:** Control access and differentiate responsibilities.

**Fields:**
- `Name`, `Description`, `IsActive`
- `Users`: List of users in this role

---

### 3. **Customer**
Represents a potential or existing client. A customer can have multiple interactions, deals, and assigned agents.

**Business Purpose:** Central object in CRM. The focal point of all sales and activity tracking.

**Fields:**
- `FirstName`, `LastName`, `Email`, `Phone`, `CompanyName`, `Address`
- `CustomerStatusId`: Foreign key to status entity
- `Notes`: Freeform notes by agents
- `LeadScore`: Numerical value indicating customer "temperature"
- `LastContactedDate`, `NextFollowUpDate`: Activity timing
- `AssignedToUserId`: Sales agent assigned to the customer

**Navigation:**
- `CustomerStatus`, `AssignedToUser`

---

### 4. **CustomerStatus**
Dynamic entity representing lifecycle stages of a customer (e.g., Prospect, Contacted, Lost).

**Business Purpose:** Track how far a lead has progressed through the sales funnel.

**Fields:**
- `Name`, `Description`, `IsActive`
- `Customers`: Reverse relation

---

### 5. **Deal**
Represents a sales opportunity related to a customer. A deal tracks value, stage, and ownership.

**Business Purpose:** The sales unit of work. Used for forecasting revenue, sales team performance, and pipeline planning.

**Fields:**
- `DealName`, `Amount`, `ExpectedCloseDate`, `Notes`
- `CustomerId`, `AssignedToUserId`, `DealStageId`

**Navigation:**
- `Customer`, `AssignedToUser`, `DealStage`

---

### 6. **DealStage**
Represents the current phase of a deal (e.g., New, In Negotiation, Won, Lost).

**Business Purpose:** Define the sales pipeline and track how deals are progressing.

**Fields:**
- `Name`, `Order`, `IsFinal`
- `Deals`: Reverse navigation

This entity replaces static enums to allow dynamic configuration of sales pipelines.

---

### 7. **Activity**
Logs any interaction with a customer, such as a call, meeting, or email.

**Business Purpose:** Allows agents to record touchpoints, plan future actions, and maintain a relationship history.

**Fields:**
- `Title`, `Description`, `ActivityDate`
- `ActivityTypeId`, `CustomerId`, `CreatedByUserId`

**Navigation:**
- `ActivityType`, `Customer`, `CreatedByUser`

---

### 8. **ActivityType**
Defines categories for activity logs (e.g., Call, Meeting, Email).

**Business Purpose:** Helps categorize interactions, useful in filtering, analysis, and activity planning.

**Fields:**
- `Name`, `Icon`, `IsActive`
- `Activities`: Reverse navigation

---

### 9. **Notification**
System-generated alerts for follow-ups or status changes.

**Business Purpose:** Drives user engagement, ensures timely actions, and improves responsiveness.

**Fields:**
- `Title`, `Message`
- `RecipientUserId`: User who receives the alert
- `IsRead`, `SentAt`, `ReadAt`
- `RelatedEntityType`: e.g., "Deal", "Activity" — stored as string for flexible linking
- `RelatedEntityId`: Optional reference ID to target object

> This structure allows notifications to be dynamically linked to any entity type, without strict foreign key enforcement.

---

## 🔮 Future Expansion Potential (per Entity)

### User
- Permissions table for fine-grained access
- User profile picture or metadata

### Role
- Role-based access control (RBAC)
- Assign default dashboards or permissions

### Customer
- Tags (many-to-many): for dynamic grouping
- Document uploads per customer
- Social media or LinkedIn links

### Deal
- Lead source tracking (e.g., Ads, Email)
- Probability percentage for forecasting
- Deal history tracking (stage transitions)

### Activity
- File attachments per activity (e.g., call recording)
- Activity templates for standard follow-ups
- Statistics dashboards per agent

### Notification
- Real-time push notifications via SignalR
- Notification preferences per user
- Email integration (SendGrid, SMTP)

---

## Final Notes

The Domain Layer defines the pure business logic and rules of the CRM without depending on infrastructure. It is the heart of the Clean Architecture. Every decision here impacts the system's scalability, clarity, and maintainability.

Each entity in SalesPilotCRM is mapped not just as a data table but as a business concept. Relationships, extensibility points, and field-level decisions are made with respect to the actual **daily workflow of sales agents, managers, and CRM operators**.

This document is intended to give deep insight into entity design, business rules, encourage proper usage, and prepare the system for future evolution.

---


