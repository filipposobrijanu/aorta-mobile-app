<h1 align="center">Aorta App</h1>
<h3 align="center">Cross-Platform Medical Informatics Mobile Application | Built with Unity & C#</h3>

<p align="center">
  A highly responsive, production-ready mobile application designed for real-time cardiovascular risk assessment. Utilizing a dual-layered algorithmic evaluation pipeline, <strong>Aorta App</strong> cross-references user biometric profiles against international clinical standards, integrating local low-latency caching alongside asynchronous cloud synchronization.
</p>

<p align="center">
  <a href="https://opensource.org/licenses/MIT">
    <img src="https://img.shields.io/badge/License-MIT-yellow.svg?style=for-the-badge" alt="License" />
  </a>
</p>

---

### ✨ Core Features

* **Dual-Layered Clinical Diagnostic Pipeline:** Implements two distinct operational validation frameworks:
  * **Baseline Assessment Module:** Evaluates raw systolic and diastolic arterial pressures utilizing the explicit logical boundaries mapped by the *American Heart Association (AHA)*.
  * **Personalized Analysis Engine:** Bypasses static physiological limits by executing a multi-parametric validation matrix. It combines hemodynamic metrics with demographic profiles (Age and Gender) backed by *European Society of Cardiology (ESC)* guidelines and the *Baptist Health Clinical Reference Chart* to account for age-induced arterial stiffness.
* **Hybrid Data Persistence Architecture:** Orchestrates a reliable storage matrix using a split caching paradigm:
  * **Local State Management:** Persists vital sign session metrics natively using Unity's `PlayerPrefs` subsystem to prevent session data loss.
  * **Asynchronous Remote Cloud Telemetry:** Establishes a non-blocking Client-Server network pipeline executing asynchronous payloads to a *Google Firebase Realtime Database* backend cluster, persisting diagnostic data in scalable, un-nested JSON trees.
* **Dynamic Runtime Internationalization (i18n):** Features an in-engine dynamic localization system supporting instantaneous, zero-reload UI updates between English and Greek languages by hot-swapping `TextMeshPro` resource configurations on the fly.
* **Robust Input Validation & Exception Handling:** Enforces strict structural integrity on input arrays using fault-tolerant parsing routines (`int.TryParse`), preventing memory exceptions and UI crashes during anomalous user inputs.

---

### 🛠️ Technical Stack

**Engine, Logic & Cloud Infrastructure**
<p align="left">
  <img src="https://img.shields.io/badge/Unity_Engine-000000?style=for-the-badge&logo=unity&logoColor=white" alt="Unity" />
  <img src="https://img.shields.io/badge/C%23_Scripting-239120?style=for-the-badge&logo=c-sharp&logoColor=white" alt="C#" />
  <img src="https://img.shields.io/badge/Google_Firebase-FFCA28?style=for-the-badge&logo=firebase&logoColor=black" alt="Firebase" />
  <img src="https://img.shields.io/badge/Android_SDK-3DDC84?style=for-the-badge&logo=android&logoColor=white" alt="Android" />
</p>

---

### 📸 Architecture & Systems Showcase

<p align="center">
  <img src="images/0.jpg" width="48%" alt="Baseline Interface View" />
  <img src="images/1.jpg" width="48%" alt="Personalized Assessment View" />
</p>
<p align="center">
  <img src="images/2.jpg" width="48%" alt="Localization and Input Validation Test" />
  <img src="images/3.jpg" width="48%" alt="Google Firebase Realtime Console Synchronization" />
</p>
<p align="center">
  <img src="images/5.jpg" width="97%" alt="Localization and Input Validation Test" />
</p>

---

### 🚀 How to Run & Build

**1. Clone the repository:**
```bash
git clone [https://github.com/filipposobrijanu/Aorta-App.git](https://github.com/filipposobrijanu/Aorta-App.git)
cd Aorta-App
