# AI-Powered Container Damage Detection & Summarization

## 📌 Description
This project uses **Azure Custom Vision** to detect and localize container damage in images. Detected damage areas are highlighted with rectangles, and the annotated image is then passed to a **Multimodal Large Language Model (LLM)** (Azure OpenAI GPT-4.1) to generate a technical summary of the damage.  
The goal is to combine **computer vision** for damage detection and **natural language generation** for concise, human-readable reports.

---

## ⚙️ How it Works
1. **Image Input** – A container image is provided as input to the system.
2. **Damage Detection (Azure Custom Vision)** –  
   - The image is sent to Azure Custom Vision Prediction API.  
   - Damage areas are identified and bounding box coordinates are returned.
3. **Drawing Rectangles** –  
   - The system draws rectangles over detected damage areas in the original image.
4. **Multimodal LLM Summarization** –  
   - The annotated image is sent to Azure OpenAI GPT-4o.  
   - The LLM analyzes the image and generates a **technical summary** describing the damage types and locations.
5. **Result Output** –  
   - The summary and optionally the annotated image are returned for reporting or further action.

---

## Input
<img width="1022" height="685" alt="image" src="https://github.com/user-attachments/assets/b15b3266-d993-44ad-a727-574d32f23601" />

## Output
<img width="637" height="423" alt="image" src="https://github.com/user-attachments/assets/86bbafad-b7dc-489b-9a9f-d984b8298377" />

---

### 1. Top Red Rectangle (Ceiling/Bulkhead Joint)

**Damage Type:**  
- Puncture / Gash

**Description:**  
- Visible hole or puncture at the ceiling and top rail junction.  
- Paint is chipped away, exposing bare metal — indicating both physical penetration and loss of protective coating.

**Potential Implications:**  
- **Water Ingress:** Hole may allow rainwater/moisture inside, risking cargo damage and internal corrosion.  
- **Structural Integrity:** Localized weakness could expand under repeated stress.  
- **Pest/Insect Entry:** Opening could allow rodents or insects inside.

**Recommended Action:**  
- Patch or weld with compatible steel, ensuring waterproof sealing.  
- Remove rust, repaint, and coat with anti-corrosion paint.  
- Inspect surrounding areas for hidden damage or corrosion.

---

### 2. Lower Red Rectangle (Wall)

**Damage Type:**  
- Surface Abrasion / Paint Loss

**Description:**  
- Small patch of scraped-off paint, exposing the underlying metal.

**Potential Implications:**  
- **Corrosion Risk:** Exposed steel can rust, especially in humid or marine environments.  
- **Progression:** If untreated, corrosion can spread and weaken the wall panel.

**Recommended Action:**  
- Clean the area and remove any early rust formation.  
- Repaint with marine-grade, anti-corrosive paint.  
- Monitor periodically for recurrence or spreading.

---

## 🖼 Architecture
```mermaid
graph LR
    A[User Uploads Image] --> B[Damage Detection]
    B --> C[Draw Bounding Rectangles]
    C --> D[Annotated Image]
    D --> E[Multimodal LLM]
    E --> F[Damage Summary]
    
    subgraph Detection
        B
        C
    end

    subgraph Analysis
        E
    end
