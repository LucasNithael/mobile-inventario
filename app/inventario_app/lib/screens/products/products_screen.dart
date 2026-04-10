import 'package:flutter/material.dart';
import 'package:inventario_app/repositories/product_repository.dart';
import '../../models/product_output.dart';
import '../../widgets/product_card.dart';
import 'product_form_screen.dart';
import 'product_movement_modal.dart';

class ProductsScreen extends StatefulWidget {
  const ProductsScreen({super.key});

  @override
  State<ProductsScreen> createState() => _ProductsScreenState();
}

class _ProductsScreenState extends State<ProductsScreen> {
  final repository = ProductRepository();
  final searchController = TextEditingController();

  List<ProductOutput> products = [];
  List<ProductOutput> filteredProducts = [];

  bool loading = true;
  String selectedCategory = "Todas";

  @override
  void initState() {
    super.initState();
    loadProducts();
  }

  Future<void> loadProducts() async {
    setState(() => loading = true);

    final result = await repository.getAll();

    setState(() {
      products = result;
      loading = false;
    });

    applyFilter();
  }

  void applyFilter() {
    final search = searchController.text.toLowerCase().trim();

    filteredProducts = products.where((p) {
      final matchName = p.name.toLowerCase().contains(search);
      final matchCategory =
          selectedCategory == "Todas" || p.category == selectedCategory;

      return matchName && matchCategory;
    }).toList();

    setState(() {});
  }

  List<String> getCategories() {
    final categories = products.map((e) => e.category).toSet().toList();
    categories.sort();
    return ["Todas", ...categories];
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Produtos"),
        actions: [
          IconButton(
            onPressed: () {},
            icon: const Icon(Icons.dark_mode_outlined),
          ),
        ],
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () async {
          await Navigator.push(
            context,
            MaterialPageRoute(builder: (_) => const ProductFormScreen()),
          );

          loadProducts();
        },
        child: const Icon(Icons.add),
      ),
      body: Padding(
        padding: const EdgeInsets.all(12),
        child: Column(
          children: [
            // Search + dropdown
            Row(
              children: [
                Expanded(
                  child: TextField(
                    controller: searchController,
                    onChanged: (_) => applyFilter(),
                    decoration: InputDecoration(
                      hintText: "Buscar produto",
                      prefixIcon: const Icon(Icons.search),
                      border: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(14),
                      ),
                    ),
                  ),
                ),
                const SizedBox(width: 12),
                DropdownButton<String>(
                  value: selectedCategory,
                  items: getCategories()
                      .map((c) => DropdownMenuItem(value: c, child: Text(c)))
                      .toList(),
                  onChanged: (value) {
                    if (value == null) return;
                    selectedCategory = value;
                    applyFilter();
                  },
                ),
              ],
            ),

            const SizedBox(height: 16),

            // List
            Expanded(
              child: loading
                  ? const Center(child: CircularProgressIndicator())
                  : RefreshIndicator(
                      onRefresh: loadProducts,
                      child: ListView.builder(
                        itemCount: filteredProducts.length,
                        itemBuilder: (context, index) {
                          final product = filteredProducts[index];

                          return ProductCard(
                            product: product,
                            onMovement: () {
                              showModalBottomSheet(
                                context: context,
                                isScrollControlled: true,
                                builder: (_) {
                                  return ProductMovementModal(
                                    productName: product.name,
                                    onConfirm: (quantity, type) async {
                                      // aqui você chama a API depois
                                      // repository.registerMovement(...)

                                      ScaffoldMessenger.of(
                                        context,
                                      ).showSnackBar(
                                        const SnackBar(
                                          content: Text(
                                            "Movimentação registrada (mock).",
                                          ),
                                        ),
                                      );
                                    },
                                  );
                                },
                              );
                            },
                            onEdit: () {},
                            onDelete: () async {
                              // aqui você chama repository.delete(product.id)
                              ScaffoldMessenger.of(context).showSnackBar(
                                const SnackBar(
                                  content: Text("Excluir (mock)."),
                                ),
                              );
                            },
                          );
                        },
                      ),
                    ),
            ),
          ],
        ),
      ),
    );
  }
}
