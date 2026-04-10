import 'package:flutter/material.dart';

class ProductMovementModal extends StatefulWidget {
  final String productName;
  final Function(double quantity, int type) onConfirm;

  const ProductMovementModal({
    super.key,
    required this.productName,
    required this.onConfirm,
  });

  @override
  State<ProductMovementModal> createState() => _ProductMovementModalState();
}

class _ProductMovementModalState extends State<ProductMovementModal> {
  final quantityController = TextEditingController();
  int selectedType = 1; // 1 = entrada, 2 = saída

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: EdgeInsets.only(
        left: 16,
        right: 16,
        top: 16,
        bottom: MediaQuery.of(context).viewInsets.bottom + 16,
      ),
      child: Column(
        mainAxisSize: MainAxisSize.min,
        children: [
          Text(
            widget.productName,
            style: const TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
          ),

          const SizedBox(height: 16),

          DropdownButtonFormField<int>(
            value: selectedType,
            decoration: const InputDecoration(
              labelText: "Tipo",
              border: OutlineInputBorder(),
            ),
            items: const [
              DropdownMenuItem(value: 1, child: Text("Entrada")),
              DropdownMenuItem(value: 2, child: Text("Saída")),
            ],
            onChanged: (value) {
              if (value == null) return;
              setState(() => selectedType = value);
            },
          ),

          const SizedBox(height: 14),

          TextField(
            controller: quantityController,
            keyboardType: TextInputType.number,
            decoration: const InputDecoration(
              labelText: "Quantidade",
              border: OutlineInputBorder(),
            ),
          ),

          const SizedBox(height: 16),

          SizedBox(
            width: double.infinity,
            child: ElevatedButton(
              onPressed: () {
                final quantity =
                    double.tryParse(quantityController.text.trim());

                if (quantity == null || quantity <= 0) {
                  ScaffoldMessenger.of(context).showSnackBar(
                    const SnackBar(
                      content: Text("Informe uma quantidade válida."),
                    ),
                  );
                  return;
                }

                widget.onConfirm(quantity, selectedType);
                Navigator.pop(context);
              },
              child: const Text("Confirmar"),
            ),
          ),
        ],
      ),
    );
  }
}