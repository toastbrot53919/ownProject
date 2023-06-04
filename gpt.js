const fs = require('fs');
const path = require('path');
const PDFDocument = require('pdfkit');

const rootDir = 'C:/Users/Toastbrot/Downloads/STRATEGY 01.04.2022/My project/Assets/Scripts';

// Recursively read files in a directory and its subfolders
function readFilesRecursively(directory) {
  const files = fs.readdirSync(directory);
  const results = [];

  files.forEach((file) => {
    const filePath = path.join(directory, file);
    const stat = fs.statSync(filePath);

    if (stat.isDirectory()) {
      results.push(...readFilesRecursively(filePath));
    } else {
      results.push(filePath);
    }
  });

  return results;
}

// Read file contents and return as a string
function readFile(filePath) {
  return fs.readFileSync(filePath, 'utf8');
}

// Find corresponding summary and syntax tree files
function findRelatedFiles(directory, filename) {
  const summaryFile = path.join(directory, `${filename}_summary.txt`);
  const syntaxTreeFile = path.join(directory, `${filename}_SyntaxTreeSummary.txt`);

  return {
    summaryFile,
    syntaxTreeFile,
  };
}

// Generate PDF from content
function generatePDF(content, filePath) {
  const doc = new PDFDocument();
  const stream = fs.createWriteStream(filePath);
  doc.pipe(stream);

  doc.font('Helvetica-Bold').fontSize(16).text('Project', { align: 'center' }).moveDown(0.5);
  doc.fontSize(12).text(content);

  doc.end();
}

// Main function to process files and generate PDFs
function generateProjectPDFs() {
  const files = readFilesRecursively(rootDir);
  let projectCodeContent = '';
  let projectSyntaxContent = '';
  let projectSummaryContent = '';

  files.forEach((file) => {
    const fileExt = path.extname(file);
    const fileName = path.basename(file, fileExt);

    if (fileExt === '.cs' || (fileExt === '.txt' && (fileName.endsWith('_summary') || fileName.endsWith('_SyntaxTreeSummary')))) {
      const relatedFiles = findRelatedFiles(path.dirname(file), fileName);

      if (fs.existsSync(relatedFiles.summaryFile) && fs.existsSync(relatedFiles.syntaxTreeFile)) {
        const fileContent = readFile(file);
        const summaryContent = readFile(relatedFiles.summaryFile);
        const syntaxTreeContent = readFile(relatedFiles.syntaxTreeFile);

        projectCodeContent += `- ${fileName} at ${file}:\nCode of file ${fileName}:\n${fileContent}\n`;
        projectSyntaxContent += `- ${fileName} at ${file}:\nCorresponding SyntaxTree:\n${syntaxTreeContent}\n`;
        projectSummaryContent += `- ${fileName} at ${file}:\nSummary of ${fileName}:\n${summaryContent}\n`;
      }
    }
  });

  generatePDF(projectCodeContent, 'projectCode.pdf');
  generatePDF(projectSyntaxContent, 'projectSyntax.pdf');
  generatePDF(projectSummaryContent, 'projectSummary.pdf');
}

generateProjectPDFs();
