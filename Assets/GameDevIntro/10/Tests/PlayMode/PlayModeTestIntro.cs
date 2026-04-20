using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CPSC386.Tests
{
  /// <summary>
  /// Introduction to Play Mode tests.
  ///
  /// A Play Mode test runs inside Unity's actual runtime: the Editor enters
  /// Play Mode, a test scene is loaded, and your test method executes frame
  /// by frame alongside Unity's normal game loop. That means Update runs,
  /// physics steps happen, coroutines resolve, and MonoBehaviour lifecycle
  /// methods (Awake, Start, OnEnable, etc.) fire exactly as they would in a
  /// running game.
  ///
  /// The signature is different from an Edit Mode test in two ways:
  ///   1. The attribute is [UnityTest] instead of [Test].
  ///   2. The method returns IEnumerator (not void), so it can yield —
  ///      yielding pauses the test for a frame, a fixed duration, or
  ///      until a condition is met.
  ///
  /// ------------------------------------------------------------------
  /// Prefer Edit Mode tests when you can.
  /// ------------------------------------------------------------------
  ///
  /// Play Mode tests are powerful but expensive:
  ///
  ///   - They are SLOW. Each test requires entering and exiting Play Mode,
  ///     which can take several seconds per test. A suite of 50 Play Mode
  ///     tests can take minutes; a suite of 50 Edit Mode tests finishes
  ///     in under a second.
  ///
  ///   - They are HARDER TO ISOLATE. Anything in the scene, any singleton,
  ///     any static state from a previous test — all of it can leak in
  ///     and cause flaky results that pass in one run and fail in the next.
  ///
  ///   - They are HARDER TO DEBUG. A failure inside a coroutine on frame
  ///     47 of a physics simulation is a very different investigation from
  ///     a failure on a single line of pure C#.
  ///
  /// The guideline: if you can express the test as pure C# logic —
  /// constructors, algorithms, state machines, data transformations —
  /// write an Edit Mode test. Reach for Play Mode only when you genuinely
  /// need the runtime: physics collisions, coroutines, frame-dependent
  /// behavior, scene loading, or MonoBehaviour lifecycle timing.
  ///
  /// ------------------------------------------------------------------
  /// To run Play Mode tests:
  ///   Window > General > Test Runner, then select the "PlayMode" tab.
  /// ------------------------------------------------------------------
  /// </summary>
  public class PlayModeTestIntro
  {
    // ---------------------------------------------------------------------
    // A single example: verify that a GameObject created at runtime moves
    // as expected after one frame.
    //
    // "yield return null" advances the test by exactly one frame. This is
    // the simplest way to let Unity's game loop do one tick of work before
    // we check the result.
    // ---------------------------------------------------------------------
    [UnityTest]
    public IEnumerator GameObject_MovesForward_AfterOneFrame()
    {
      // Arrange: create an object and give it a component that moves it.
      GameObject go = new GameObject("TestMover");
      Mover mover = go.AddComponent<Mover>();
      mover.SpeedPerFrame = 1f;

      yield return null;
      Vector3 startPosition = go.transform.position;

      // Act: let one frame pass so Update() runs.
      yield return null;

      // Assert: the object moved forward by the expected amount.
      Assert.That(
        go.transform.position.x,
        Is.EqualTo(startPosition.x + 1f).Within(0.0001f)
      );

      // Cleanup: Play Mode tests do NOT automatically tear down objects
      // you create. Leaked GameObjects will pollute later tests. Always
      // destroy what you spawn.
      Object.Destroy(go);
    }
  }

  // -----------------------------------------------------------------------
  // A minimal MonoBehaviour used by the Play Mode test above.
  //
  // This has to be a MonoBehaviour (not a plain C# class) because the test
  // needs Update() to run on it — which is precisely the kind of thing
  // that justifies a Play Mode test in the first place.
  // -----------------------------------------------------------------------
  public class Mover : MonoBehaviour
  {
    public float SpeedPerFrame = 1f;

    void Update()
    {
      transform.position += new Vector3(SpeedPerFrame, 0f, 0f);
    }
  }
}
