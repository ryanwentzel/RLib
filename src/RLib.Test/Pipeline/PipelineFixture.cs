using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RLib.Pipeline
{
    [TestFixture]
    public class PipelineFixture
    {
        private class Sentence
        {
            public string Value { get; set; }

            public Sentence()
            {
                Value = string.Empty;
            }
        }

        private class AppendText : IFilter<Sentence>
        {
            private readonly string text;

            public AppendText(string sentence)
            {
                this.text = sentence;
            }

            public void Execute(Sentence context, Action<Sentence> executeNext)
            {
                context.Value += text;

                executeNext(context);
            }
        }

        [Test]
        public void Should_Execute_Filters_In_Order_Applied()
        {
            var sentence = new Sentence { Value = "" };

            var pipeline = new Pipeline<Sentence>();
            pipeline.Add(new AppendText("The"))
                    .Add(new AppendText(" brown"))
                    .Add(new AppendText(" fox."))
                    .Execute(sentence);

            Assert.That(sentence.Value, Is.EqualTo("The brown fox."));
        }
    }
}
