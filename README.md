# Simulation of Human Movement and Humanoid Robotics

This repository contains a Unity Project (6000.6) for practicing various concepts related to the simulation of human movement and humanoid robotics.

## Development

This project uses [CSharpier](https://csharpier.com/) for code formatting.

Install by running:
```bash
dotnet tool restore
```

To format the code, run:
```bash
dotnet csharpier format Assets/Scripts
```

# Attributions
This repository includes prototype textures from Kenney.nl licensed under Creative Commons CC0.
https://kenney.nl/assets/prototype-textures

# Inverse Kinematics (IK)
## With the Unity Animation Rigging Package

### Getting started
https://docs.unity3d.com/Packages/com.unity.animation.rigging@6.6/manual/RiggingWorkflow.html
This package is used to create procedural animation at runtime through the use of rig constraints.

This tutorial is for an older version of the package, so it may not be up to date.
https://learn.unity.com/tutorial/working-with-animation-rigging

This tutorial helps fill in some of the gaps in the Unity Learn lesson.
https://medium.com/@sean.duggan/unity-animation-rigging-setting-it-up-from-basics-53f2eb79b667

For IK, we can use the Two Bone IK Constraint described in Step 7 of the tutorial.

Note that the setup of the Two Bone IK Constraint can be simplified by using the "Auto Setup from
Tip Transform" option in the context menu for the component after assigning the Right_Hand transform
to the `Tip` field.
https://docs.unity3d.com/Packages/com.unity.animation.rigging@6.6/manual/constraints/TwoBoneIKConstraint.html
