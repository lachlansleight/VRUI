# VRUI
For look targets AKA fuse buttons. Does anybody even use these anymore? Oh and dmm management.

**dmmCanvas prefab usage:**
* Canvas y position should match camera y position
* Canvas x, y and z scales should match Canvas z offset from camera position

**LookButton prefab usage:**
* Must be a child (can be several layers deep) of a VRUI_Canvas object.
* Cooldown Time Modifier at zero means when the cursor leaves, the look counter resets to zero

**dmm:**
* One dmm is one millimeter when viewed at 1m. Moving a canvas closer means that one dmm gets physically smaller, and vice versa
* This means that if the canvas is 1m from the main camera, it should be scaled as (1, 1, 1). If it is 5m from the main camera, it should be scaled as (5, 5, 5)
* So don't mess with the dmmScale gameobject!

**ergonomics:**
* The Eye and Neck canvases show comfortable canvas sizes for a player to move only their eyes (EyeCanvas) or their eyes and neck (NeckCanvas). UI elements should stay within these areas.
* DropCenter is used to make the entire canvas slightly lower - a standard measure of 100dmm down from even horizontal is used.

---

### Useful dmm sizes according to Google (and modified / added-to by me)

Category | Item | dmm Size |
| :--- | --- | ---:|
| Panel | Eye Comfortable Vertical Center | -100dmm |
| | Eye Comfortable Width | 1200dmm |
| | Eye Comfortable Height | 1200dmm |
| | Neck Comfortable Vertical Center | -300dmm |
| | Neck Comfortable Width | 3250dmm |
| | Neck Comfortable Height | 2200dmm |
| | | |
| Text | Headline | 40dmm |
| | Title | 32dmm |
| | Subheading | 28dmm |
| | Body | 24dmm |
| | Caption | 20dmm |
| | Button | 28dmm |
| Look Targets | Comfortable Look Target | 192dmm |
| | Minimum Look Target | 128dmm |
| | Look Target Padding | 32dmm |
| | | |
| Point Targets | Comfortable Point Target | 96dmm |
| | Minimum Point Target | 64dmm |
| | Point Target Padding | 16dmm |
