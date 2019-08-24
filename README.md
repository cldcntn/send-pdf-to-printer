# send-pdf-to-printer
Send directly pdf documents (if your printer support it) to a network printer using port 9100 

# Result
If you want to test a network printer if it's capable of directly print out pdf documents.
Program use standard 9100 port.
If printer is able to print your pdf you can send it any pdf document from any device directly to it.
If printer can't recognize pdf probably it will print some text included on the header of the pdf.
In this case in the first line you'll read something like "%PDF-1.4" and probably 
you'll need to stop the printer to avoid waste of paper.

# Usage
TestPdfToPrinter.exe pdfFileName.pdf ipAddress

# Why
Print documents created by a web application directly to the printer.
