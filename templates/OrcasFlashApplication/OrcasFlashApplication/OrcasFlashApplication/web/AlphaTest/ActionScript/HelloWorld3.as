package AlphaTest.ActionScript
{
    import flash.display.Sprite;
    import flash.text.TextField;

    public class HelloWorld3 extends Sprite
    {
        public function HelloWorld3()
        {
			super();
        
            var txt:* = new TextField();
            
            txt.text = "hello world";
            
            addChild(txt);
        }
    }
}