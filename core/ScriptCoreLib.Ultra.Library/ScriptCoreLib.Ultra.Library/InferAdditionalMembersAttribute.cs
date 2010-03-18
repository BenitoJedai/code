using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Library
{
	[global::System.AttributeUsage(AttributeTargets.Interface, Inherited = false, AllowMultiple = true)]
	public sealed class InferAdditionalMembersAttribute : Attribute
	{
		// http://www.google.com/search?q=define:infer
		// guess: guess correctly; solve by guessing; "He guessed the right number of beans in the jar and won the prize"

		// look for each implementing type
		// look how they define the implementations
		// figure out if there are more members just like it
		// add those missing members

		// if the implementation is implementing the member with an explanation
		// how other members could be discovered that knowlodge should be used

		// this implementation shall be defined by jsc.meta /Rewrite and should be applied to assemblies copied to SDK path
		
		// at a later stage we could even have IDL based rewriter.
		// "This is what I have. This is why I have it. Update me."

		// but the first step is to go after events
		// then maybe async definitions?

		// [Hint, Reason, Why, From what, Where others can be found]
		// inferring, enhancer should be applied to scriptcore lib also

		// note the chicken and egg problem.
		// jsc.meta should always statically link to the manually defined items
		// it could reuse the enhanced versions at runtime tho provided the manually defined items are still there
		// we will defenetly need to fix the foreach support.
	}
}
