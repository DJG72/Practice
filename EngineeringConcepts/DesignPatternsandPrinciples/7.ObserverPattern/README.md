# Exercise 7: Implementing the Observer Pattern

## 👨‍💻 Developer Info
- **Name**: DEBJIT GHOSH
- **Assignment**: Cognizant Digital Nurture 5.0
- **Skill**: Design Patterns and Principles

---

## 🧠 Problem Statement

Develop a Stock Market Monitoring System where multiple clients need to be notified whenever stock prices change.

The Observer Pattern is used to establish a one-to-many relationship between a stock market (subject) and multiple applications (observers).

Whenever stock prices are updated, all registered observers receive notifications automatically.

---

## ✅ Objectives

- Create a Subject interface for stock management.
- Create an Observer interface for subscribers.
- Implement observer registration and removal.
- Notify all observers whenever stock prices change.
- Demonstrate dynamic subscription management.

---

## 🏗️ Implementation Details

### 👨‍🔧 Interfaces & Classes

#### Subject Interface

- `IStock`

Methods:

- `RegisterObserver()`
- `DeregisterObserver()`
- `NotifyObservers()`

---

#### Observer Interface

- `IObserver`

Method:

- `Update()`

---

#### Concrete Subject

- `StockMarket`

Responsibilities:

- Maintain observer list
- Register observers
- Remove observers
- Notify observers when stock prices change

---

#### Concrete Observers

- `MobileApp`
- `WebApp`

Responsibilities:

- Receive stock price updates
- Display notifications

---

## 🛠️ Pattern Details

| Pattern Name | Observer Pattern |
|--------------|------------------|
| Category | Behavioral Pattern |
| Intent | Define a one-to-many dependency between objects |
| Usage | Event notification systems |
| Benefit | Loose coupling between subject and observers |

---

## 🔄 Observer Structure

```text
                    +----------------+
                    |  StockMarket   |
                    +----------------+
                           |
            ---------------------------------
            |                               |
            v                               v
      +------------+                 +------------+
      | MobileApp  |                 |  WebApp    |
      +------------+                 +------------+
```

---

## 📈 Workflow

### Register Observers

```csharp
stockMarket.RegisterObserver(mobileUser);
stockMarket.RegisterObserver(webUser);
```

---

### Update Stock Price

```csharp
stockMarket.SetStock("TCS", 3850.50);
```

---

### Automatic Notification

```text
Stock Updated
      ↓
NotifyObservers()
      ↓
MobileApp Updated
      ↓
WebApp Updated
```

---

### Remove Observer

```csharp
stockMarket.DeregisterObserver(webUser);
```

Only remaining observers will receive future updates.

---

## 📸 Output Screenshot

Below is a sample output after running the program:

![Output](./Output/Output.png)

---

## 🧪 How to Run

```bash
cd DesignPatternsandPrinciples/7.ObserverPattern/Code
dotnet run
```

---

## 🎯 Expected Output

```text
Observer Pattern - Stock Market Monitoring System

Stock Updated: TCS = ₹3850.5
Mobile App [Rahul] Notification: TCS price changed to ₹3850.5
Web App [Priya] Notification: TCS price changed to ₹3850.5

Stock Updated: Infosys = ₹1625.75
Mobile App [Rahul] Notification: Infosys price changed to ₹1625.75
Web App [Priya] Notification: Infosys price changed to ₹1625.75

Removing Web App Observer...

Stock Updated: Wipro = ₹540.25
Mobile App [Rahul] Notification: Wipro price changed to ₹540.25
```

---

## 🎓 Conclusion

The Observer Pattern enables automatic notification of multiple dependent objects whenever the state of a subject changes.

This pattern is widely used in:

- Stock market applications
- Event handling systems
- News subscription services
- Real-time dashboards
- Publish-subscribe architectures

By keeping subjects and observers loosely coupled, the system becomes flexible, scalable, and easy to maintain.