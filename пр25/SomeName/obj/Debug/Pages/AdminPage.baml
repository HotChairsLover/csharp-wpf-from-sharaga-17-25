   M S B A M L   `   `   `  ����C  ?SomeName, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:SomeName.PagesSomeName.Pages  5          SomeName.Pages.AdminPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:SomeName.Pages     5        N� Title$  	AdminPage��5	      .��5      � 
�������.+�5   
   �� $��Пользователи��6   $��10q�6'   $��20(�63   $
��Bold"�6A   6U   �� 5   
   -   5        $��
Выйти��5      $	��150��6.   $��25��6:   $	��Top=�6F   $��Right=�6^   $��10q�6z   6�   �� 5   
   -   5         ��Name$ usersListView��5      $��	0,50,0,50q�6*    �� View 5      �� 5      .P�5      �� $��Код��6%    �� Width$ 75��62    �� DisplayMemberBinding 6=   �� *index+6f   �� 5      $��
Логин��6%   $ 75��64    6?   �� *login+6h   �� 5      $��Пароль��6%   $ 75��65    6@   �� *
password+6l   �� 5      $��Роль��6%   $ 75��63    6>   �� *roll+6f   5      5      �� 5   
   -   5        $��Добавить��5   !   $	��150��64   $��25��6@   $��Bottom=�6L   $��Right=�6g   $��
0,0,400,10q�6�   6�   �� 5   
   -   5        $��Изменить��5   "   $	��150��65   $��25��6A   $��Bottom=�6M   $��Right=�6h   $��
0,0,200,10q�6�   6�   �� 5   
   -   5        $��Удалить��5   $   $	��150��66   $��25��6B   $��Bottom=�6N   $��Right=�6i   $��10q�6�   6�   5      5      