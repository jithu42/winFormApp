   M S B A M L   `   `   `  ����P  LHamburgerMenuApp.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:HamburgerMenuApp.Core.ViewsHamburgerMenuApp.Core.Views  5      (    "HamburgerMenuApp.Core.Views.leaves   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aF 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:HamburgerMenuApp.Core.Views     5      -   5        
����ح�.��5	      � .+�5
   
   �� 
����·�$��2q�6'   $
��Left=�6;   $	��531��6V   $��	10,13,0,0q�6c   $	��Top=�6v   $	��970��6�   �� 5   
   -   5          ��Name$  label2_Copy4��5      $��
Department��6&   $
��Left=�6;   $��	30,29,0,0q�6V   $	��Top=�6i   $��Times New Roman)�6�   $��15(�6�   $
��Bold"�6�   �� 5   
   -   5        $  label4_Copy3��5      $��Semester��6&   $
��Left=�69   $��
340,29,0,0q�6T   $	��Top=�6h   $��Times New Roman)�6�   $��15(�6�   $
��Bold"�6�   �� 5   
   -   5         ��Name$ search_class_dept��5      $
��Left=�6.   $��
132,30,0,0q�6I   $	��Top=�6]   $	��179��6u    J� RenderTransformOrigin$ -5.742,3.115&�6�   .@�5      �� $��--Select Department--��6    �� 
IsSelected . 6;   �� 5      $	��BCA��6   �� 5      $	��BBM��6   �� 5      $
��BCOM��6   5      �� 5   
   -   5        $ 
search_sem��5      $
��Left=�6'   $��
424,30,0,0q�6B   $	��Top=�6V   $	��179��6n   $ -5.742,3.115&�6z   .@�5      �� $��--Select Semester--��6    . 69   �� 5      $��1��6   �� 5      $��2��6   �� 5      $��3��6   �� 5      $��4��6   �� 5      $��5��6   �� 5      $��6��6   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ assign_grid��5      $
��Left=�6(      AutoGenerateColumns .  $	��932��6`   $
��Auto��6m   $��
30,159,0,0q�6{   $	��Top=�6�      
IsReadOnly .    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ id��6%   	   
Visibility$	 Hidden=�61   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6E   �� *id+   ElementStyle 5      �� 5       .�5!      ��  �� Property � �� �� Value$
 Wrap=�6G   �� 5"       �@��TextAlignment$ Center=�6H   5#      5%        5&      $ Std Reg No.��6%   
 6:   �� *reg_no+ 5'      �� 5(      .�5)      ��  � ��$
 Wrap=�6G   �� 5*       �@��TextAlignment$ Center=�6H   5+      5-        5.      $ Std Name��6%   
 67   �� *
leave_from+ 5/      �� 50      .�51      ��  � ��$
 Wrap=�6G   �� 52       �@��TextAlignment$ Center=�6H   53      55        56      $ Faculty Name��6%   
 6;   �� *
leave_to+ 57      �� 58      .�59      ��  � ��$
 Wrap=�6G   �� 5:       �@��TextAlignment$ Center=�6H   5;      5=        5>      $ Class��6%   
 64   �� *dept+ 5?      �� 5@      .�5A      ��  � ��$
 Wrap=�6G   �� 5B       �@��TextAlignment$ Center=�6H   5C      5E        5F      $	 Sem��6%   
 62   �� *sem+ 5G      �� 5H      .�5I      ��  � ��$
 Wrap=�6G   �� 5J       �@��TextAlignment$ Center=�6H   5K      5M        5N      $ Subject��6%   
 66   �� *leave_subject+ 5O      �� 5P      .�5Q      ��  � ��$
 Wrap=�6G   �� 5R       �@��TextAlignment$ Center=�6H   5S      5U        5V      $ Reason��6%   
 65   �� *reason+ 5W      �� 5X      .�5Y      ��  � ��$
 Wrap=�6G   �� 5Z       �@��TextAlignment$ Center=�6H   5[      5]        5^      $ 
Leave From��6%   
 69   �� *	from_date+ 5_      �� 5`      .�5a      ��  � ��$
 Wrap=�6G   �� 5b       �@��TextAlignment$ Center=�6H   5c      5e        5f      $ Leave To��6%   
 67   �� *	to_date+ 5g      �� 5h      .�5i      ��  � ��$
 Wrap=�6G   �� 5j       �@��TextAlignment$ Center=�6H   5k      5m        5n      $ Leave Status��6%   
 6;   �� *leave_status+ 5o      �� 5p      .�5q      ��  � ��$
 Wrap=�6G   �� 5r       �@��TextAlignment$ Center=�6H   5s      5u        5v      $ Faculty Message��6%   
 6>   �� *faculty_message+ 5w      �� 5x      .�5y      ��  � ��$
 Wrap=�6G   �� 5z       �@��TextAlignment$ Center=�6H   5{      5}      5      �� 5�   
   -   5         ��Name$ search_leaves��5�      $��Search��6(   $
��Left=�69   
����·�$��132,111,0,0q�6i   $	��Top=�6~   $��87��6�   $��Times New Roman)�6�   $��11(�6�   $
��Bold"�6�   6�   �� 5�   
   -   5        $ btn_search_clear��5�      $��Clear��6+   $
��Left=�6;   
����·�$��236,111,0,0q�6k   $	��Top=�6�   $��75��6�   $��Times New Roman)�6�   $��11(�6�   $
��Bold"�6�   $��13��6   �� 5�   
   -	   5        $  
leave_date��5�      $��Leave Applied��6$   $
��Left=�6<   $��	30,73,0,0q�6W   $	��Top=�6j   $��Times New Roman)�6�   $��15(�6�   $
��Bold"�6�   (  "System.Windows.Controls.DatePicker  5�   
   -
   5          Name$ 	from_date��5�      |�.  $
��Left=�6:   $��
132,74,0,0q�6U   $	��Top=�6i   $	��179��6�   5�      5�      