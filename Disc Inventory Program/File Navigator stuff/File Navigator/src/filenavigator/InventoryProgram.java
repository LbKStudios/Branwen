package filenavigator;

import java.awt.event.ActionEvent;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;

/**
 *
 * @author Saurutobi
 */
public class InventoryProgram extends javax.swing.JFrame
{
    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JTextField directoryPath;
    private javax.swing.JButton inventoryButton;
    private javax.swing.JScrollPane jScrollPane2;
    private javax.swing.JTextArea jTextArea1;
    // End of variables declaration//GEN-END:variables
    
    private String curPath;
    private String [] directories = new String[1];;
    private String [] files = new String[1];
    static private FileWriter output;
    String [] unsorted;
    
    public InventoryProgram()
    {
        initComponents();
    }
    
    public static void main(String args[]) throws IOException
    {
        output = new FileWriter(new File("test.csv"));
        /* Set the Nimbus look and feel */
        //<editor-fold defaultstate="collapsed" desc=" Look and feel setting code (optional) ">
        /* If Nimbus (introduced in Java SE 6) is not available, stay with the default look and feel.
         * For details see http://download.oracle.com/javase/tutorial/uiswing/lookandfeel/plaf.html 
         */
        try {
            for (javax.swing.UIManager.LookAndFeelInfo info : javax.swing.UIManager.getInstalledLookAndFeels()) {
                if ("Nimbus".equals(info.getName())) {
                    javax.swing.UIManager.setLookAndFeel(info.getClassName());
                    break;
                }
            }
        } catch (ClassNotFoundException ex) {
            java.util.logging.Logger.getLogger(InventoryProgram.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (InstantiationException ex) {
            java.util.logging.Logger.getLogger(InventoryProgram.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (IllegalAccessException ex) {
            java.util.logging.Logger.getLogger(InventoryProgram.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (javax.swing.UnsupportedLookAndFeelException ex) {
            java.util.logging.Logger.getLogger(InventoryProgram.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        }
        //</editor-fold>

        /* Create and display the form */
        java.awt.EventQueue.invokeLater(new Runnable() {
            public void run() {
                new InventoryProgram().setVisible(true);
            }
        });
    }
    
    private void inventoryButtonActionPerformed(java.awt.event.ActionEvent evt)
    {
        try
        {
            curPath = //use the goddamn filechooser thing for this
            Inventory(new File(curPath));
        }
        catch(Exception e)
        {
            System.out.println("fuck this shit, I broke");
        }
    }

    public void Inventory(File parent) throws IOException
    {
        File curFile;
        curPath = parent.getAbsolutePath() + File.separator;
        unsorted = parent.list();
        System.out.println("i got here");
        String fileName = "";
        String fileExtension = "";
        String filePath = "";
        String [] toSend = new String[3];
        
        curFile = new File(curPath + unsorted[0]);
        if (curFile.isDirectory())
        {   
            directories[0] = "[dir ]" + unsorted[0];
        }
        else
        {
            files[0] = "[dir ]" + unsorted[0];
        }
        
        String [] temp;
        for (int i = 0; i < unsorted.length; i++)
        {
            curFile = new File(curPath + unsorted[i]);
            if (curFile.isDirectory())
            {
                temp = directories;
                directories = new String[temp.length + 1];
                for(int j = 0; j < temp.length; j++)
                {
                    directories[j] = temp[j];
                }
                directories[directories.length - 1] = "[dir ]" + unsorted[i];
            }
            else
            {
                temp = files;
                files = new String[temp.length + 1];
                for(int j = 0; j < temp.length; j++)
                {
                    files[j] = temp[j];
                }
                files[files.length - 1] = "[dir ]" + unsorted[i];
            }
        }
        
        for(int i = 0; i < directories.length; i++)
        {
            curFile = new File(curPath + directories[i]);
            Inventory(curFile);
        }
        
        for (int i = 0; i < files.length; i++)
        {
            curFile = new File(curPath + files[i]);
            fileName = curFile.getAbsolutePath();
            fileExtension = fileName.split(".")[1];//substring(fileName.length() - 4, fileName.length() - 1);
            filePath = curFile.getAbsolutePath();//pull filename out of there and fix it
            toSend[0] = fileName;
            toSend[1] = fileExtension;
            toSend[2] = filePath;
            print(toSend);
        }
    }
    
    public void print(String [] toPrint) throws IOException
    {
        System.out.println(toPrint[0] + "," + toPrint[1] + "," + toPrint[2] + "/n");
        output.write(toPrint[0] + "," + toPrint[1] + "," + toPrint[2] + "/n");
    }
    
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents()
	{

        directoryPath = new javax.swing.JTextField();
        jScrollPane2 = new javax.swing.JScrollPane();
        jTextArea1 = new javax.swing.JTextArea();
        inventoryButton = new javax.swing.JButton();

        setDefaultCloseOperation(javax.swing.WindowConstants.EXIT_ON_CLOSE);

        directoryPath.setText("Enter Directory Path to use");
        

        jTextArea1.setColumns(20);
        jTextArea1.setRows(5);
        jScrollPane2.setViewportView(jTextArea1);

        inventoryButton.setText("Inventory");
        inventoryButton.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                inventoryButtonActionPerformed(evt);
            }
        });

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(jScrollPane2)
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(directoryPath, javax.swing.GroupLayout.PREFERRED_SIZE, 215, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addGap(18, 18, 18)
                        .addComponent(inventoryButton, javax.swing.GroupLayout.PREFERRED_SIZE, 139, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addGap(0, 371, Short.MAX_VALUE)))
                .addContainerGap())
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(inventoryButton)
                    .addComponent(directoryPath, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addGap(1, 1, 1)
                .addComponent(jScrollPane2, javax.swing.GroupLayout.DEFAULT_SIZE, 430, Short.MAX_VALUE)
                .addContainerGap())
        );

        pack();
    }// </editor-fold>//GEN-END:initComponents

}
