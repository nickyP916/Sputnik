
namespace Draw.Tests
{
    public class ShapeDrawerShould
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Draw_a_shape_with_given_inputs()
        {
            var x = 5;
            var y = 6;
            var shape = '\u25A0';
            var colour = ConsoleColor.Magenta;
            ShapeDrawer.DrawShape(x, y, shape, colour);
        }
    }
}