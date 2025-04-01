# Azure Pipeline appx & apk & exe & aab

### All 4 builds in one pipeline. apk(android) exe(windows) appx(microsoft store) aab(googleplay store)

This project provides an automated pipeline to build ALL 4 using Unity. The pipeline leverages Azure DevOps for CI/CD and outputs the generated All 4 files and symbols to the D:/ .It’s designed for setups where the agent pool runs on a Windows Server.

(here are the complete detailed steps to create your Windows agent and Azure agent pool 👉 https://learn.microsoft.com/en-us/azure/devops/pipelines/agents/windows-agent?view=azure-devops)


### Structure
Build Pipeline (YAML) (azure-pipeline.yaml file at the root of your repository): Configured to trigger on the main branch and run on the unity-build-h agent pool. DONT FORGET TO MODIFY

Build Script (C#) (BuildScript.cs at Assets/Editor/): Uses Unity’s build API to generate the AAB and handle keystore settings. DONT FORGET TO MODIFY

Modify the output paths or file names directly in BuildScript.cs.

Happy Building! :)

