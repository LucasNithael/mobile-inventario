import 'package:flutter/material.dart';

class ProductFormScreen extends StatelessWidget {
  const ProductFormScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text("Novo Produto")),
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: Column(
          children: [
            TextField(
              decoration: InputDecoration(
                labelText: "Nome do produto",
                border: OutlineInputBorder(
                  borderRadius: BorderRadius.circular(12),
                ),
              ),
            ),
            const SizedBox(height: 14),
            DropdownButtonFormField(
              items: const [
                DropdownMenuItem(value: "1", child: Text("Eletrônicos")),
                DropdownMenuItem(value: "2", child: Text("Ferramentas")),
              ],
              onChanged: (value) {},
              decoration: InputDecoration(
                labelText: "Categoria",
                border: OutlineInputBorder(
                  borderRadius: BorderRadius.circular(12),
                ),
              ),
            ),
            const SizedBox(height: 14),
            TextField(
              keyboardType: TextInputType.number,
              decoration: InputDecoration(
                labelText: "Quantidade mínima",
                border: OutlineInputBorder(
                  borderRadius: BorderRadius.circular(12),
                ),
              ),
            ),
            const SizedBox(height: 18),
            SizedBox(
              width: double.infinity,
              child: ElevatedButton(
                onPressed: () {},
                child: const Text("Salvar"),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
