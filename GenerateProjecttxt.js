const fs = require('fs');
const path = require('path');

const directoryPath = 'C:/Users/Toastbrot/Downloads/STRATEGY 01.04.2022/My project/Assets/Scripts';

function processFile(filePath, outputPath) {
  const fileName = path.basename(filePath, path.extname(filePath));
  const summaryFilePath = path.join(path.dirname(filePath), `${fileName}_summary.txt`);
  const syntaxTreeFilePath = path.join(path.dirname(filePath), `${fileName}_SyntaxTreeSummary.txt`);
  
  const content = `- ${fileName} at ${filePath}:\n` +
    `Summary of ${fileName}:\n${fs.readFileSync(summaryFilePath, 'utf-8')}\n` +
    `Code of file ${fileName}:\n${fs.readFileSync(filePath, 'utf-8')}\n` +
    `Corresponding SyntaxTree:\n${fs.readFileSync(syntaxTreeFilePath, 'utf-8')}\n`;
  
  fs.appendFileSync(outputPath, content);
}

function processDirectory(directoryPath, outputPath) {
  fs.readdirSync(directoryPath).forEach((file) => {
    const filePath = path.join(directoryPath, file);
    const stat = fs.statSync(filePath);
    
    if (stat.isDirectory()) {
      processDirectory(filePath, outputPath);
    } else {
      if (file.endsWith('.cs') || file.endsWith('_summary.txt') || file.endsWith('_SyntaxTreeSummary.txt')) {
        processFile(filePath, outputPath);
      }
    }
  });
}

const outputPath = path.join(directoryPath, 'project.txt');
fs.writeFileSync(outputPath, ''); // Create an empty project.txt file

processDirectory(directoryPath, outputPath);
