   M S B A M L   `   `   `  ����  
JobVacancy%    JobVacancy.EditJobVacancyWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$3  -Редактирование вакансии��5   	   $	��450��6)   $	��800��66   .��5      �� $��10q�6    �� Orientation$ Vertical=�6   .+�5	   
   �� .+�5
      �� $
��Auto��6   $
��Left=�6&   .+�5      �� $ 
Horizontal=�6   $��5q�66   $
��Auto��6B   .+�5      �� $ Vertical=�6!   .+�5      �� $ 
Horizontal=�6%   .+�5      �� $��Тип вакансии��6$   $��Center=�6<   $	��160��6W   �� 5      $	��120��6'    �� DisplayMemberPath$
 Type��6U    �� SelectedValuePath$
 Type��5      ��5   3   �� *
JobTypes+ �� SelectedItem 5   0   �� *JobVacancy.JobType+5      �� 5      $ 
Horizontal=�6%   .+�5      �� $��Зарплата��6$   $
��Left=�67   $��Center=�6R   $	��160��6m   �� 5       �� TextWrapping$
 Wrap=�6&   $��Center=�6;   $	��120��6V   ��5      ��  �� Path$ JobVacancy.Patch�5      �� 5      $ 
Horizontal=�6%   .+�5      �� $!��Сфера вакансии��6$   $
��Left=�6=   $��Center=�6X   $	��160��6s   �� 5      -   5         ��Name$ JobAreaTextBox��5   &   $
 Wrap=�6>    �� 
IsReadOnly$
 True��6R   $��Center=�6d   $	��120��6   ��5      �� $ JobVacancy.JobArea.Title��� 5      -   5        $��Выбрать��5   %   $��20��6Y   $��5 0 0 0q�6e   5      �� 5!      $ 
Horizontal=�6%   .+�5"      �� $��Должность��6$   $
��Left=�68   $��Center=�6S   $	��160��6n   �� 5#      -   5        $ JobTitleTextBox��5#   &   $
 Wrap=�6?   $
 True��6S   $��Center=�6e   $	��120��6�   ��5$      �� $ JobVacancy.JobTitle.Title��� 5%      -   5        $��Выбрать��5%   %   $��20��6Z   $��5 0 0 0q�6f   5&      5'      �� 5(      $ Vertical=�6!   $��5q�68   .+�5)      �� $'��!Особые требования��6$   $
��Left=�6@   $��Center=�6[   $	��160��6v   �� 5*      $
 Wrap=�6&   $��Center=�6<   $��75��6X   $	��400��6d   ��5+      �� $$ JobVacancy.SpecialRequirements�5,      5-      5.      5/      �� 51   
   $��10q�6   $
��Auto��6!   $
��Auto��6.   $ Vertical=�6<   .+�52      �� $
��Auto��6   $��80��6'   .+�53      �� $��3q�6   $ 
Horizontal=�6(   $��Stretch=�6A   .+�54      �� $��Работодатель��6   $
��Left=�63   $��Center=�6N   $	��160��6i   �� 55      -   5        $��Изменить��55      $
��Auto��6S   $��20��6`   56      &   System.Windows.Controls.DataGrid  57      -   5        	  Name$	 EmployerGrid��57      
   AutoGenerateColumns$
 False��61      
IsReadOnly$
 True��6M      Columns 58      0  *System.Windows.Controls.DataGridTextColumn  5:      ,  &System.Windows.Controls.DataGridColumn   Header$ Телефон��6G      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6X   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6-   �� *Phone+6X     5;      $ 
Адрес��6I   $ * 6X    6-   �� *	Address+6X     5<      $' !Название компании��6M   $ * 6h    6-   �� *CompanyName+6h   5>      5?      5@      �� 5A   
   -   5        $��Сохранить��5A      $��Right=�6%   $	��100��6d   5B      5C      