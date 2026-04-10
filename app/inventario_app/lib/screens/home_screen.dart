import 'package:flutter/material.dart';
import 'products/products_screen.dart';
import 'categories/categories_screen.dart';
import 'movements/movements_screen.dart';
import 'purchases/purchases_screen.dart';

class HomeScreen extends StatefulWidget {
  const HomeScreen({super.key});

  @override
  State<HomeScreen> createState() => _HomeScreenState();
}

class _HomeScreenState extends State<HomeScreen> {
  int index = 0;

  final screens = const [
    ProductsScreen(),
    CategoriesScreen(),
    MovementsScreen(),
    PurchasesScreen(),
  ];

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: IndexedStack(index: index, children: screens),
      bottomNavigationBar: BottomNavigationBar(
        currentIndex: index,
        onTap: (value) => setState(() => index = value),
        type: BottomNavigationBarType.fixed,
        items: const [
          BottomNavigationBarItem(
            icon: Icon(Icons.inventory_2_outlined),
            label: "Produtos",
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.category_outlined),
            label: "Categorias",
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.swap_horiz),
            label: "Movimentações",
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.shopping_cart_outlined),
            label: "Compras",
          ),
        ],
      ),
    );
  }
}
