# 🚗 AI Powered Car Information Extraction API (POC)

A Proof-of-Concept (POC) application built in **C#** that:
1. Receives a car image via an API endpoint.
2. Uses **Azure OpenAI GPT-4.1 Multimodal** to extract the vehicle registration number from the image.
3. Calls an **MCP (Model Context Protocol) Server** to retrieve detailed car information.
4. Returns the details .

This project demonstrates **multimodal AI**, **Semantic Kernel orchestration**, and integration with **external services** for a real-world use case.

## 🛠️ Tech Stack

- C#
- .NET 8
- Model Context Protocol (MCP)
- Azure
- Azure OpenAI GPT-4.1 (Multimodal)
- Semantic Kernel


## 🏗️ Architecture Overview

```mermaid
flowchart LR
    A[Client] --> B[API Endpoint - .NET 8 C#]
    B --> C[Service Layer]
    C --> D[Azure OpenAI GPT-4.1 Multimodal Agent]
    D --> E[Extract Registration Number]
    E --> F[MCP Server]
    F --> G[Vehicle Details Response]
    G --> A
