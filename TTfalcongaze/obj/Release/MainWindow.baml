   M S B A M L   `   `   `  ���� G  CTTfalcongaze, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:TTfalcongazeTTfalcongaze      TTfalcongaze.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:TTfalcongaze       � Title$  Students Journal��$	��350��$	��525�� � WindowStartupLocation$ CenterScreen=� � 	Resources  
  MainDT � ColumnDefinitions � RowDefinitions �� Orientation �� Path �� StringFormat��%�   &	        �� .R�� $��5q� �� $
��auto � �� $
��auto ��� $
��auto �.+��� $ 
Horizontal=�$��0��.+��� $��2q����� $ LastName��� $��2q����� $ 	FirstName��� $��1��$��2q����� $	 Age�$ 
{0} лет��.���  �� ��  �� $
��Auto ��� .+�o� $	��Top=�$��0��$��2��.@�m� $��Управление��.@�m� -    m�Name$ AddStudentMenuItem��$'��!Добавить студента��m� -   $ EditStudentMenuItem��$ ��Редактировать��{�.  m� -   $ DeleteMenuItem��$��Удалить��{�.  �� -   	 ��Name$	 NotElementTextBlock��$��1��$��Center=�$��Center=�$��20(�$y�Hidden=�$��Список пуст���� -   
 ��Name$
 StudentsListBox�� �� AlternationCount$ 2��$��1��#��[   �� SelectionMode$ Extended=�����  �� 
TargetType" ��.���  �� Property � �� �� Value$	 5,3q���  � ��$ 5q���  � ��
 ��İ���  � �� ��  �� 
TargetType" ��.R���  ��Name$	 brd��#��z��#��z��#��z��._��� #��z�� �� Triggers P�  P� Property �@��AlternationIndex P� Value$ 0��. ���  � ��
 � ���P�  �@��
IsSelected . . ���  � ��
 �2�2���  � ��
 �������  � ��$ 1q���  � ��
 �   �� -    �Name$ StudentGrid��$��1��$��1��.+��� $��1��.+��� $��Имя���� -    ��Name$ StudentNameTextBlock�� �� 
IsReadOnly . ���� $ 	FirstName��� $��Фамилия���� -   $ StudentLastNameTextBlock�� . ���� $ LastName��� $��Возраст���� -	   $ StudentAgeTextBlock�� . ���� $	 Age�$ 
{0} лет��