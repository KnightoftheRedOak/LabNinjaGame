Sprite Breaker - Unity Component
http://gogoat.com/sprite-breaker
support: sprite.breaker.help@gmail.com

Sprite Breaker's purpose is to split Sprites into MeshRenderer-based shards to simulate object breakage with minimum
effort or maintenance.

Breaking can be done in editor, or at run time by calling Break() on the component. Shards' shapes can be generated
randomly at break time, or defined using included Sprite Breaker Data editor.

Shards can then be simulated using 2d or 3d physics (using colliders and rigidbodies created on break), or using
"fake physics" which ignores collisions. Shards can fade and deactivate after set number of seconds.


BASIC SETUP

1. Add a GameObject to scene with SpriteRenderer attached. Assign a sprite to the SpriteRenderer component.
2. Add SpriteBreaker component to this GameObject.
3. In Inspector check "Break on Awake" checkbox
4. Press Play in editor
   
TERMINOLOGY

SpriteBreaker:
    the main component added to GameObject that facilitates generation of shards.

Shards:
    objects generated by SpriteBreaker meant to look like fragments of the original sprite.

Sprite Breaker Data:
    contains actual shapes of the polygons used to create shards. Can be contained in SpriteBreaker component, or saved
    and reused as an asset of type SpriteBreakerDataAsset.

Breaker Button:
    calls Break() method on the component. Generates shards according to current Breaker Data.

Clear Button:
    calls Clear() method on the component. Destroys any created shards.

Sprite Breaker Data Asset:
    contains Sprite Breaker Data in a format that can be stored in project Assets. Can be used to reuse the same shard
    shapes between multiple different sprites, or for setting the same starting shape for radial shard generator.   
    
Sprite Breaker Editor:
    A tool included with this package, for manually defining and editing shard shapes in Sprite Breaker Data.
    

SPRITE BREAKER COMPONENT INSPECTOR

Sprite:
    if this is assigned to a Sprite asset, SpriteBreaker will use this sprite instead of looking for SpriteRenderer
    component on this GameObject.
    
Shard Prototype:
    optional prefab for a shard object. If you want your shards to have other scripts or behaviors attached on spawn,
    create a prefab and assign it to this field.
    
Shards Parent:
    if you want shards to be parented to a transform other than this GameObject, set it here.
    
Break on Awake:
    call Break() when this GameObject is added to the scene.

Breaker Data / Edit Data:
    click this button to open Sprite Breaker Editor to edit current shards shapes.
    
Breaker Data / Clear Data:
    deletes current Breaker Data.
    
Breaker Data / Asset / Load Data:
    if Sprite Breaker Data Asset is assigned, current Breaker Data will be replaced with that from the asset.
    
Breaker Data / Asset / Save to (or Replace) Asset:
    saves current data to a new Sprite Breaker Data Asset, or replaces/overwrites the one in the field.
    
Generate / Seed:
    if zero, a random seed will be used for generating shatter effect each time, otherwise uses the provided seed.
    
Generate / Use:
    Starting shape for generating shards.
    Quad - use basic full quad
    Data - use current data as the starting point
    Asset - use assigned asset (if assigned) as the starting point 
    
Generate / Randomness:
    the amount of initial shard shape irregularity in the generation algorithm.
     
Generate / Angle:
    starting angle for radial shards.
    
Generate / Spacing:
    initial spacing in degrees between shards. Higher number = fewer shards.
    
Generate / Frequency:
    how often to subdivide each radial shard. Higher number = more subdivisions.
    
Generate / Origin:
    center of the generated shards on the sprite.
    
Generate / Preview:
    shows what Breaker Data will look like. You can click on the preview to change Origin.
    
Generate / Generate Shards:
    click to replace current Breaker Data with generated polygons.
    
Appearance / Keep Shards Color Updated:
    if checked, shards material's _Color property will be updated each frame with SpriteBreaker.shardsColor value
    
Appearance / Shadow Casting Mode:
    shards MeshRenderer.castShadows will be set to this value on spawn, unless using shard prefab with MeshRenderer.
    
Appearance / Receive Shadows:
    shards MeshRenderer.receiveShadows will be set to this value on spawn, unless using shard prefab with MeshRenderer.
    
Appearance / Disable SpriteRenderer On Break:
    if there's a SpriteRenderer attached to this GameObject, it will be disabled when shards are spawned, and re-enabled
    if SpriteBreaker component is reset.
    
Appearance / Draw Editor Gizmos:
    draw Breaker Data polygons in the editor.
    
Simulation / Time To Live:
    total lifetime for all the shards in seconds.
    
Simulation / Fade After:
    shards will begin to fade (using material _Color property) after this number of seconds.
    
Simulation / End Action:
    what happens after Time To Live seconds elapses.
    Destroy Object - destroy the GameObject that SpriteBreaker is attached to
    Destroy Parent - destroy shards parent transform GameObject if set, otherwise destroy this transform's parent
    Deactivate Shards - each shards' GameObject will be setActive to false.
    Reset - destroy shards and re-enable SpriteRenderer.
    Clear Data And Reset - destroy shards, re-enable SpriteRenderer, and delete Breaker Data.
    Restart - destroy shards, re-create shards and restart simulation.
    Nothing - do nothing.
    
Simulation / Physics:
    type of simulation to run on generated shards. Affects what additional components are created on shards.
    Collider 2D - use 2D physics. Creates PolygonCollider2D and RigidBody2D.
    Collider 3D - use 3D physics. Creates MeshCollider and RigidBody.
    Fake Physics - simulates shards falling down using internal Update method. No colliders.
    
Simulation / Collider Thickness:
    if Collider 3D is selected, this will define the collision mesh's thickness used by MeshCollider.
    
Simulation / Mass Multiplier:
    the physics mass of the simulated shard is its polygon's area multiplied by this number.
    
Simulation / Initial Impulse:
    initial impulse applied to each shard:
    Radial - shard is pushed away from the sprite's center
    Linear - shard is pushed in X,Y, or Z direction
    Rotational - shard's initial rotational velocity
    
Events / OnShardCreated( GameObject, SpriteBreakerPolygon, SpriteBreaker ):
    emitted for each created shard. Can be used for additional initialization. GameObject is the shard itself, 
    SpriteBreakerPolygon contains data used to spawn this shard from Breaker Data, and SpriteBreaker is this component.
    
Events / OnTimeToLiveExpired( GameObject ):
    emitted on each shard when Simulation / Time To Live has elapsed.
    
Advanced / CSG Upscale:
    Used by internal algorithm performing CSG (polygon cutting and Booleans) to premultiply points coordinates.
    
Advanced / CSG Epsilon:
    Epsilon precision value used during CSG. Represents 10 to the -X power (or 10e-X).
    
    
SPRITE BREAKER EDITOR

Sprite Breaker Editor is used to manually edit the polygon data used to create shards. It opens in a floating
window, and displays the sprite from SpriteRenderer component attached to this GameObject, or Sprite from SpriteBreaker
component, if it's overridden. On top of the sprite there can be one or more polygons, representing the shards that will
be created on break. If the current sprite breaker data is empty, this will be populated from sprite's outline shape.  

Inside the window use mouse middle or right button to pan, mouse wheel to zoom, and left mouse button to select points
and polygons.

On top of the window there are three buttons that toggle current mode - Create, Edit, and Cut.

Reset Sprite button on the far top right will reset the current sprite data to sprite's custom outline shape.

Sprite Breaker Editor respects Unity's undo system.

Create mode:
    Clicking in the editor will begin a new polygon. Place three or more points and right click, or hit Enter or Escape 
    to finish. Hover mouse near an existing point to snap to it while creating.
    
    Press Tab while adding points to toggle polygon direction.

Edit mode:
    Switching to Edit will display an additional toggle to the right, between Move, Rotate, and Scale. 
    
    Clicking on a point or a polygon will select it. Shift + Click will select multiple. Shift + Click + drag in empty
    space will begin selecting using a marquee. To deselect, click in empty space.
    
    Dragging a point or a polygon will either move, rotate, or scale it based on what's currently selected on top.
   
    Holding Shift during move or scale restricts moving along one axis. During Rotate it will snap to 15 deg increments.
    
    Keyboard arrow keys will move selected points or polygons up, down, left, and right. Hold Shift to move faster.
    
    Inside each polygon there's a "+" shaped icon. It represents the pivot point of the shard that will be created. It
    can be moved around relative to the polygon.
    
    Right-clicking on a polygon will bring up its properties:
        Enabled: whether a shard will be created for this polygon
        Active: whether the shard GameObject will be active
        Physics: participate in simulation
        Name: GameObject name assigned to the shard
        Sorting Order: MeshRenderer sortingOrder is set to the sortingOrder of the SpriteRenderer + this value 
        Z Offset: shard's transform z offset
        Pivot: shard's pivot relative to the polygon
        Tag: shard GameObject tag
        Float Value: passed to OnShardCreated callback in SpriteBreakerPolygon class
        String Value: passed to OnShardCreated callback in SpriteBreakerPolygon class
    
    Double clicking a polygon will isolate it for editing. This can be helpful when there are many polygons nearby.

    Select two or more overlapping polygons to perform Combine, Subtract, and Intersect boolean operations. The buttons
    will appear in the tool bar area on top.
    
    Hold Control and click on a polygon edge to insert a point. Clicking will place this point, and will start inserting
    another. Press Tab or Shift+Tab to change direction/cycle though segments. Enter, Escape, or right click to finish.

Cut mode:
    Click in empty space or on a point to start defining a cutting line. Place two or more points and right click, or 
    hit Enter or Escape to finish. Editor will split intersecting polygons using this line. If you're working with an
    isolated polygon, only that polygon will be affected. 
    
    Note that you can't make straight up holes in polygons. You can fake them however.


SPRITE BREAKER API

The following methods are available for SpriteBreaker component instances:

Break()
    Uses current breaker data (or SpriteBreakerDataAsset from dataAsset field) to generate shards and begin simulation.
    
Clear()
    Deletes all spawned shards, and re-enables SpriteRenderer.
    
Generate()
    Uses generation parameters to replace current breaker data with generated polygons.
        
RestartSimulation(bool resetShardPositions=true)
    Resets clock for timeToLive and fadeAfter, and restarts physics simulation.
    resetShardPositions parameter makes shards move to their spawn positions.
    
The following public properties are available for SpriteBreaker component instances:

    SpriteBreakerData data
        Object containing the shard shape data.
    
    SpriteBreakerDataAsset dataAsset
        If assigned, SpriteBreaker without data will copy data from asset when broken.

    Sprite sprite
        Sprite used to generate fragments. If this field is not assigned, a sprite from attached SpriteRenderer will 
        be used.

    GameObject shardPrototype
        An optional gameObject prefab used to generate shards.

    Transform shardsParent
        If set, this transform used as a parent for generated shards.

    bool updateShardsColor
        Update spawned shards renderer color every frame.
        
    ShadowCastingMode castShadows
        Shards' MeshRenderer shadow casting mode.
       
    bool receiveShadows
        Shards' MeshRenderer shadow receive mode.
       
    Color shardsColor
        Overall color/alpha of all shards, multiplied into each spawned fragment's renderer.

    ShardEvent OnShardCreated
        This event is called for each generated shard.
   
    bool drawEditorGizmos
        Determines if Unity Editor should draw shards preview.

    Color gizmoColor
        Draw shards preview with this color.

    bool disableRendererOnBreak
        Should attached SpriteRenderer be disabled/enabled on Break/Clear.

    CreateColliders createColliders
        If set, a collider and rigidbody will be added/updated to each shard with polygon edges. 
        One of CreateColliders enum: None, Collider2D, Collider3D, FakePhysics.        

    float colliderThickness
        For 3D collider how thick should the generated collider mesh be.
   
    Vector3 fakeGravity
        Gravity for fake physics createColliders mode.

    float massMultiplier
        Rigid bodies mass will be computed by multiplying this value by the shard polygon area. If this value is
        negative, the mass is set as an absolute value (polygon size doesn't affect mass). 
   
    float initialRadialImpulse
        Apply this impulse radially from origin to each shard on break.
        
    float initialRadialImpulsePlusMinus
        Plus or minus randomness % variation for radial impulse.
   
    Vector3 initialLinearImpulse
        Apply this impulse linearly to each shard on break.
        
    float initialLinearImpulsePlusMinus
        Plus or minus randomness % multiplier for linear impulse.

    Vector3 initialRotationalImpulse
        Apply this impulse to shard rotation on break.
        
    float initialRotationalImpulsePlusMinus
        Plus or minus randomness % multiplier for rotational impulse.
   
    float timeToLive
        Time to live (seconds). If set to non-0, the shards will be destroyed after this time interval after Break()
        is called.

    float fadeAfter
        If timeToLive is set, the shards will fade to alpha=0 after this time.

    float timeBroken
        returns Time.time value when Break() was called.

    EndAction endAction
        If timeToLive is set, what to do when time expires after Break().
        One of EndAction enum values: DestroyObject, DestroyParent, DeactivateShards, Reset,
        ClearDataAndReset, Restart, DoNothing
   
    BreakerEvent OnTimeToLiveExpired
        If timeToLive is set, this event is called after time expires.

    float csgUpscale
        How much to upscale coordinates when performing CSG operations

    int csgEpsilon
        Epsilon precision value used during CSG. 10 to the -X power (or 10e-X), where X is this value.

    int generateSeed
        Generation seed - if set to 0, the seed is random

    BreakerGenerateFrom generateFrom
        Generation start point. One of BreakerGenerateFrom enum: Quad, Data, Asset
    
    float generateRandomness
        Generation randomness multiplier

    float generateAngle
        Generation features start angle
   
    float generateSpacing
        Generation features spacing

    float generateFrequency
        Generation features frequency (breaks shards into smaller pieces)
    
    Vector2 generateOrigin
        Generation origin point (0.5, 0.5 is center of the sprite)

    GameObject[] shards
        returns currently spawned shards as GameObjects
        
    bool autoBreakOnAwake
        automatically calls Break() on Awake
        
    bool hasShards
        returns true if GameObjects are attached to polygons
    
    bool canBreak
        returns true if breaking is possible - i.e. there's a sprite          