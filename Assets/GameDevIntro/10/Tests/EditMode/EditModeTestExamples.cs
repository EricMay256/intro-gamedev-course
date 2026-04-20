using NUnit.Framework;

namespace CPSC386.Tests
{
  /// <summary>
  /// Introduction to Edit Mode tests using the Unity Test Framework (UTF).
  ///
  /// Edit Mode tests run inside the Unity Editor without entering Play Mode.
  /// They execute quickly, in isolation, and are well-suited for verifying
  /// pure C# logic: algorithms, data structures, constructors, and any code
  /// that does not depend on Unity's runtime loop (Update, physics, coroutines,
  /// scene lifecycle, etc.).
  ///
  /// To run these tests:
  ///   Window > General > Test Runner, then select the "EditMode" tab.
  ///
  /// If this is your first test file, Unity will prompt you to create an
  /// "Editor" test assembly when you place this script inside an "Editor"
  /// folder (or a folder with an EditMode test assembly definition). Let
  /// the Test Runner create that folder for you via the "Create EditMode
  /// Test Assembly Folder" button; it will wire up the asmdef correctly.
  /// </summary>
  public class EditModeTestExamples
  {
    // ---------------------------------------------------------------------
    // Example 1: A simple passing test.
    //
    // The [Test] attribute marks a method as an Edit Mode test. The method
    // must be public, return void, and take no parameters. By convention,
    // test method names describe the scenario and the expected outcome.
    // ---------------------------------------------------------------------
    [Test]
    public void Addition_TwoAndTwo_EqualsFour()
    {
      int result = 2 + 2;

      // Assert.That is the "constraint-based" assertion style. It reads
      // naturally (left-to-right, like English) and is the form Unity's
      // own documentation uses. "Is.EqualTo(4)" is the constraint.
      Assert.That(result, Is.EqualTo(4));
    }

    // ---------------------------------------------------------------------
    // Example 2: A deliberately failing test.
    //
    // This test is written to FAIL so you can see what a failure looks like
    // in the Test Runner. Red X, failure message, expected vs. actual values.
    // Delete or correct this test once you've seen it fail once.
    // ---------------------------------------------------------------------
    [Test]
    public void Addition_TwoAndTwo_IntentionallyWrong()
    {
      int result = 2 + 2;

      // This assertion is wrong on purpose. The Test Runner will report:
      //   Expected: 5
      //   But was:  4
      Assert.That(result, Is.EqualTo(5));
    }

    // ---------------------------------------------------------------------
    // Example 3: The older "classic" assertion style.
    //
    // You will see this form in a lot of older code and tutorials. It still
    // works, but prefer the constraint-based form (Assert.That) in new code.
    // ---------------------------------------------------------------------
    [Test]
    public void Addition_ClassicAssertStyle_Passes()
    {
      int result = 2 + 2;

      // Equivalent to Assert.That(result, Is.EqualTo(4));
      Assert.AreEqual(4, result);
    }

    // ---------------------------------------------------------------------
    // Example 4: Testing a constructor — passing case.
    //
    // The HealthPool class below (defined at the bottom of this file) is a
    // plain C# class — not a MonoBehaviour. Edit Mode tests are ideal for
    // plain-C# logic because you can construct the object directly without
    // needing a scene, a GameObject, or the Play Mode lifecycle.
    //
    // This test verifies that the constructor correctly initializes state.
    // ---------------------------------------------------------------------
    [Test]
    public void HealthPool_Constructor_InitializesCurrentToMax()
    {
      HealthPool pool = new HealthPool(maxHealth: 100);

      Assert.That(pool.Current, Is.EqualTo(100));
      Assert.That(pool.Max, Is.EqualTo(100));
      Assert.That(pool.IsAlive, Is.True);
    }

    // ---------------------------------------------------------------------
    // Example 5: Testing a constructor — failing case.
    //
    // This test is also deliberately wrong. The expectation does not match
    // what the constructor actually produces. Run it, observe the failure,
    // then fix the expected value to see it turn green.
    //
    // In practice, failing tests like this are often how you catch bugs:
    // the code behaves one way, the test expects another, and the Test
    // Runner tells you exactly where the mismatch is.
    // ---------------------------------------------------------------------
    [Test]
    public void HealthPool_Constructor_IntentionallyWrongExpectation()
    {
      HealthPool pool = new HealthPool(maxHealth: 50);

      // The constructor sets Current = Max, so Current will be 50, not 0.
      // This assertion will fail, and the Test Runner will show you why.
      Assert.That(pool.Current, Is.EqualTo(0));
    }

    // ---------------------------------------------------------------------
    // Example 6: Testing a constructor that should throw.
    //
    // Sometimes the correct behavior of a constructor is to REJECT bad
    // input by throwing an exception. The test passes when the exception
    // is thrown, and fails if the constructor lets the bad input through.
    // ---------------------------------------------------------------------
    [Test]
    public void HealthPool_Constructor_NegativeMax_Throws()
    {
      Assert.That(
        () => new HealthPool(maxHealth: -10),
        Throws.TypeOf<System.ArgumentOutOfRangeException>()
      );
    }
  }

  // -----------------------------------------------------------------------
  // A minimal plain-C# class used by the tests above.
  //
  // Kept in the same file for teaching convenience. In a real project, this
  // would live in its own file under Assets/_Scripts/ (or wherever your
  // runtime code lives), and the test assembly would reference the runtime
  // assembly via its asmdef.
  // -----------------------------------------------------------------------
  public class HealthPool
  {
    public int Max { get; }
    public int Current { get; private set; }
    public bool IsAlive => Current > 0;

    public HealthPool(int maxHealth)
    {
      if (maxHealth < 0)
      {
        throw new System.ArgumentOutOfRangeException(
          nameof(maxHealth),
          "maxHealth cannot be negative."
        );
      }

      Max = maxHealth;
      Current = maxHealth;
    }
  }
}
