# startography
Charts and workflows are made and used at http://www.draw.io

# Dealing with Scale
## Common_Scale.xml
The Common_Scale.xml file represents the States of the environment scale.  Essentially it is the layers
that exist within any given frame, with the State being represented by the lowest layer number.  Visually, only
current scale layer and higher layers (meaning farther distance) are active at any one time.  So a state is
responsible for activating any child layers.

## Layer Scale Data
These data represent the unit scale equivalency of real world values, such as kilometres, astronomical units,
Julian light hours, days, and years, and equates them to the number of applicable virtual scale units they
should be represented with when in any given scale State.

1. 1M Km
  * 1,000,000 km = 10,000 units || 1,000 km = 10 units || 1 km = 0.01 units
2. 1 AU
  * 1 AU = 10,000 units || 149,597.8707 km = 10 units || 149.5978707 km = 0.01 units
3. 1 LH
  * 1 LH = 10,000 units || 1,079,252.8488 km = 10 units || 1,079.2528488 km = 0.01 units
4. 1 LD
  * 1 LD = 10,000 units || 25,902,068.371 km = 10 units || 5,902.068371 km = 0.01 units
5. 1 JLY
  * 1 JLY = 10,000 units || 9,460,730,472.6 km = 10 units || 9,460,730.4726 km = 0.01 units
6. 10 JYL
  * 10 JLY = 10,000 units || 94,607,304,725.808km = 10 units || 94,607,304.725808km = 0.01 units
7. 100 JLY
  * 100 JLY = 10,000 units || 946,073,047,258.08km = 10 units || 946,073,047.25808km = 0.01 units
8. 1000 JLY
  * 100 JLY = 10,000 units || 9,460,730,472,580.8km = 10 units || 9,460,730,472.5808km = 0.01 units

# Layers
## General Information
The layers to be assigned are equavalent to the scale States available.  For each different scale available,
there is one layer.  Each layer has its own camera, with distance clamping between its minimum and maximum
visibility ranges.
Positional data should be attained using the globalized values multiplied or divided by the local scale value.
This way we ensure that accuracy of object positions are maintained with less drift due to small floating
point errors.
Small layers (as in they display more of space) go to the background and the layers/cameras that capture
less space go to the foreground.
Determine the size of the viewing area with the following math:
(10,000 / ScaleOfNextSmallest) * scale
#### Example 1:
We're comparing with the "1M KM" scale which has a scale of 1x1x1.  Camera clip is 0.01 near, 10,000 far.
10M KM scale is 0.1 (1,000,000/10,000,000)
(10,000 / 1) * 0.1 = 1,000.  This is the near clipping point for the 10M KM camera.  Therefore, the camera
clipping planes are 1,000 near by 10,000 far
#### Example 2:
50M KM scale is 0.02 (1,000,000/50,000,000)
(10,000 / 0.1) * 0.02 = 2,000.  This is the near clipping point for the 50M KM camera.  Therefore, the
camera clipping planes are 2,000 near and 10,000 far.

#### Real:
##### 1M KM
###### 1M KM
  * Scale: 1x1x1
  * Distance: 1,000,000 km
  * Camera Clip Near: 0.01
  * Camera Clip Far: 10,000

###### 1 AU
  * Scale: 0.00668458712227 x 0.00668458712227 x 0.00668458712227
  * Distance: 149,597,870.7 km
  * Camera Clip Near: 66.8458712227
  * Camera Clip Far: 10,000

##### 1 LH
  * Scale: 0.00092656693111 x 0.00092656693111 x 0.00092656693111
  * Distance: 1,079,252,848.8 km
  * Camera Clip Near: 1386.12439955057353
  * Camera Clip Far: 10,000

##### 1 LD
  * Scale: 0.00003860695546 x 0.00003860695546 x 0.00003860695546
  * Distance: 25,902,068,371 km 
  * Camera Clip Near: 416.66666663518846
  * Camera Clip Far: 10,000

##### 1 JLY
  * Scale: 0.00000010570008 x 0.00000010570008 x 0.00000010570008
  * Distance: 9,460,730,472,600 km
  * Camera Clip Near: 27.37850699196263
  * Camera Clip Far: 10,000

##### 10 JLY
  * Scale: 0.000000010570008 x 0.000000010570008 x 0.000000010570008
  * Distance: 94,607,304,726,000 km
  * Camera Clip Near: 1,000
  * Camera Clip Far: 10,000

##### 100 JLY
  * Scale: 0.0000000010570008 x 0.0000000010570008 x 0.0000000010570008
  * Distance: 946,073,047,260,000 km
  * Camera Clip Near: 1,000
  * Camera Clip Far: 10,000

##### 1,000 JLY
  * Scale: 0.00000000010570008 x 0.00000000010570008 x 0.00000000010570008
  * Distance: 9,460,730,472,600,000 km
  * Camera Clip Near: 1,000
  * Camera Clip Far: 10,000

## Naming Convention

### Layer Names
I'm working with the idea that there are two ways in which I should be identifying the layers.  The first is to
have layer names that quickly identify the local scale that it represents.  For example, in the case of the "1
JLY" layer, we could name it "Julian Light Year".
The second option would be to keep it simple for programming, by naming the layers numerically.  "1 JLY" in
this case would simply be named "Layer 5" as it is the 5th layer scale.

### Object Names
Object names don't necessarily need to represent the layer in which they are currently bound.  It is likely that
we will add items at a certain scale into a parent container object.  As distances increase or descrease and
scale States change, the objects would then shift into the next layer up or down respectively.

## Visibility and (De)Activation
Only layers equal or greater (distant) to the layer that is currently the active State should be shown.  The 
visibility frustrum on the camera should be clamped between maximum distance represented by the layer just below,
and the minimum distance represented by the layer above.

## Switching States
When switching States, the objects within the current state/layer are likely to migrate into the layer either
above or below, depending on the direction required.

# Other
Perhaps allow the user to swtich between deterministic physics over to live one-body system/2 body
solver.  Are perturbations accounted for with deterministic physics?

