   M S B A M L   `   `   `  ����  Deal    Deal.EditDealWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$/  )Редактирование сделки��5   	   $	��450��6'   $	��800��64   .��5      �� $��10q�6    �� Orientation$ Vertical=�6   .+�5	   
   �� .+�5
      �� $
��Auto��6   $
��Left=�6&   .+�5      �� $ 
Horizontal=�6   $��5q�66   .+�5      �� $ Vertical=�6!   .+�5      �� $ 
Horizontal=�6%   .+�5      �� $��Дата сделки��6$   $
��Left=�6:   $��Center=�6U   $	��160��6p   (  "System.Windows.Controls.DatePicker  5      $��Center=�6)   $��Center=�6F   $	��120��6a      Text 5      ��  �� Path$ Deal.DealDate� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5      �� 5      $ 
Horizontal=�6%   .+�5      �� $��Подрядчик��6$   $
��Left=�68   $��Center=�6S   $	��160��6n   �� 5       �� TextWrapping$
 Wrap=�6&   $��Center=�6;   $	��120��6V   ��5      �� $ Deal.Сontractor�5      5      5      5      5       �� 5"   
   $��10q�6   $
��Auto��6!   $
��Auto��6.   $ Vertical=�6<   .+�5#      �� $
��Auto��6   $��80��6'   .+�5$      �� $��3q�6   $ 
Horizontal=�6(   $��Stretch=�6A   .+�5%      �� $��Вакансия��6   $
��Left=�6/   $��Center=�6J   $	��160��6e   �� 5&      -   5        $��Изменить��5&      $
��Auto��6U   $��20��6b   5'      &   System.Windows.Controls.DataGrid  5(      -   5          Name$ JobVacancyGrid��5(         
IsReadOnly$
 True��63   	   AutoGenerateColumns$	 False��6E   
   Columns
 5)      0  *System.Windows.Controls.DataGridTextColumn  5*      ,  &System.Windows.Controls.DataGridColumn   Header$ Тип вакансии��6N      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6d   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6-   �� *JobType.Type+6d     5+      $ Должность��6P   $ * 6c    6-   �� *JobTitle.Title+6c     5,      $ 
Сфера��6O   $ * 6^    6-   �� *JobArea.Title+6^     5-      $ Компания��6V   $ * 6h    6-   �� *Employer.CompanyName+6h     5.      $ Зарплата��6G   $ * 6Y    6-   �� *Patch+6Y   50      51      �� 53      $
��Auto��6   $��80��6'   .+�54      �� $��3q�6   $ 
Horizontal=�6(   $��Stretch=�6A   .+�55      �� $��Соискатель��6   $
��Left=�61   $��Center=�6L   $	��160��6g   �� 56      -   5        $��Изменить��56      $
��Auto��6T   $��20��6a   57        58      -   5        $ ApplicantGrid��58      $
 True��62   $	 False��6D   
 59        5:      $ Имя��6K   $ * 6X    6-   �� *	FirstName+6X     5;      $ Фамилия��6J   $ * 6[    6-   �� *
LastName+6[     5<      $ Отчество��6L   $ * 6^    6-   �� *
Patronymic+6^     5=      $+ %Уровень образования��6O   $ * 6l    6-   �� *EducationType+6l     5>      $  Специальность��6R   $ * 6i    6-   �� *Speciality.Title+6i     5?      $' !Учебное заведение��6S   $ * 6n    6-   �� *Institution.Title+6n   5B      5C      5D      �� 5E   
   -   5        $��Сохранить��5E      $��Right=�6%   $	��100��6^   5F      5G      