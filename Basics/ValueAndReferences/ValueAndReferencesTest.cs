namespace Basics.ValueAndReferences
{
    public class ValueAndReferencesTest
    {
        /// <summary>
        /// Basic example of value type
        /// </summary>
        [Fact]
        public void NativeValueType()
        {
            // native value types are things like bool, int, decimal, etc
            // for more: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/built-in-types

            int a = 5;
            int b = a;

            a++;

            Assert.Equal(6, a);
            Assert.Equal(5, b);
        }

        /// <summary>
        /// Basic example of reference type
        /// </summary>
        [Fact]
        public void NativeReferenceType()
        {
            // native reference types are things like object, string, dynamic

            // a is a pointer to "hello world"
            var a = "hello world";

            // b is assigned to the point to "hello world"
            var b = a;

            // a is re-assigned a pointer to "something else"
            a = "something else";

            Assert.NotEqual(a, b);
        }

        /// <summary>
        /// Objects passed to methods are defaulted to pass by value in C#
        /// </summary>
        [Fact]
        public void PassByValue()
        {
            const string originalName = "fido";
            const string newName = "peanut";

            var dogClass = new DogClass { Name = originalName };
            var dogStruct = new DogStruct { Name = originalName };

            // we define methods that will change the dog's name
            void ChangeName(IDog dog)
            {
                dog.Name = newName;
            }

            // by default all objects passed  to methods are "pass by value"
            // meaning a copy of the value is being passed 

            // first, we examine reference types
            // the value that is being copied and passed in is a pointer to the dog object
            ChangeName(dogClass);
            Assert.Equal(newName, dogClass.Name);

            // next the value type
            // the value being copied and passed in is a copy of the dog struct
            // because a copy of the dog is passed in and changed, the original dog struct maintains it's original name
            ChangeName(dogStruct);
            Assert.Equal(originalName, dogStruct.Name);
        }

        /// <summary>
        /// C# allow you to explicitly define passing by reference
        /// </summary>
        [Fact]
        public void PassByReference()
        {
            const string originalName = "fido";
            const string newName = "peanut";

            var dogClass = new DogClass { Name = originalName };
            var dogStruct = new DogStruct { Name = originalName };

            // we define methods that will change the dog's name
            // this time we specify it is pass-by-reference
            void ChangeDogClassName(ref DogClass dog)
            {
                dog.Name = newName;
            }

            void ChangeDogStructName(ref DogStruct dog)
            {
                dog.Name = newName;
            }

            // this time we are not passing the "value" of the reference 
            // we are passing access or the "reference" of the reference 
            // in this case, there is no difference in our result compared to "pass by value" 
            ChangeDogClassName(ref dogClass);
            Assert.Equal(newName, dogClass.Name);

            // we pass a reference to the original dog struct
            // unlike the pass-by-value example, this time the name of our original dog struct will change
            ChangeDogStructName(ref dogStruct);
            Assert.Equal(newName, dogStruct.Name);
        }

        // in summary: 
        //                              value types     |||  reference types
        // -----------------------------------------------------------------------
        // pass by value            original unchanged  |||  original changed
        // pass by reference        original changed    |||  original changed
    }
}
