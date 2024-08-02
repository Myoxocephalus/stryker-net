using Microsoft.CodeAnalysis;
using Stryker.Configuration.Mutants;
using System.Collections.Generic;

namespace Stryker.Configuration.ProjectComponents
{
    public class CsharpFileLeaf : ProjectComponent<SyntaxTree>, IFileLeaf<SyntaxTree>
    {
        public string SourceCode { get; set; }

        /// <summary>
        /// The original unmutated syntax tree
        /// </summary>
        public SyntaxTree SyntaxTree { get; set; }

        /// <summary>
        /// The mutated syntax tree
        /// </summary>
        public SyntaxTree MutatedSyntaxTree { get; set; }

        public override IEnumerable<Mutant> Mutants { get; set; }

        public override IEnumerable<SyntaxTree> CompilationSyntaxTrees => MutatedSyntaxTrees;

        public override IEnumerable<SyntaxTree> MutatedSyntaxTrees => new List<SyntaxTree> { MutatedSyntaxTree };

        public override IEnumerable<IFileLeaf<SyntaxTree>> GetAllFiles()
        {
            yield return this;
        }

        public override void Display()
        {
            DisplayFile(this);
        }
    }
}
