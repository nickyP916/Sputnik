using FakeItEasy;
using Sputnik.Services;

namespace Sputnik.Tests
{
    class ListenForNumberInputShould
    {
        [TestCase("13", new[] { 1, 3 })]
        [TestCase("50", new[] { 5, 0 })]
        [TestCase("11", new[] { 1, 1 })]
        public void Return_the_correct_numbers_from_console(string consoleInput, int[]? expected)
        {
            var fakeInputListener = A.Fake<IInputListener>();
            var fakeShapeDrawer = A.Fake<IShapeDrawer>();
            var playMechanicsService = A.Fake<IPlayMechanicsService>();
            var patternMatch = new PatternMatch(fakeShapeDrawer, playMechanicsService);

            A.CallTo(() => fakeInputListener.ListenForInput(A<CancellationToken>.Ignored)).Returns(consoleInput);

            var result = patternMatch.ConvertStringInputToNumbers(consoleInput);

            Assert.That(result, Is.EquivalentTo(expected));
        }

        [TestCase("138", null)]
        [TestCase("no", null)]
        [TestCase("", null)]
        public void Return_null_if_2_valid_numbers_are_not_given_to_console(string consoleInput, int[]? expected)
        {
            var fakeInputListener = A.Fake<IInputListener>();
            var fakeShapeDrawer = A.Fake<IShapeDrawer>();
            var playMechanicsService = A.Fake<IPlayMechanicsService>();
            var patternMatch = new PatternMatch(fakeShapeDrawer, playMechanicsService);

            A.CallTo(() => fakeInputListener.ListenForInput(A<CancellationToken>.Ignored)).Returns(consoleInput);

            var result = patternMatch.ConvertStringInputToNumbers(consoleInput);

            Assert.That(result, Is.Null);
        }
    }
}
